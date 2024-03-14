using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DBconnection_namespace;
using Backed.Models;
using policyConfigurations_pnamespace;
using Backed.Services;
using Backed.Utills;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DBconn>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//==============================REGISTERING SERVICES=============================================
builder.Services.AddScoped<Jwt>();
builder.Services.AddScoped<DBconn>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IEventSubscriberService, EventSubscriberService>();
builder.Services.AddScoped<EmailService>();

//=========================AUTHORIZATION=========================================================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var issuer = builder.Configuration["JwtOptions:Issuer"];
        var audience = builder.Configuration["JwtOptions:Audience"];
        var secretKey = builder.Configuration["JwtOptions:secrete_Key"];

        if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secretKey))
        {
            // Log or handle the case where configuration values are missing or null
            throw new ApplicationException("JWT configuration values are missing or null.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    PolicyConfiguration.ConfigurePolicies(options);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bidding Service", Version = "v1" });

    // Add JWT Authentication support in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Add the following line to register controller services
builder.Services.AddControllers();

var app = builder.Build();

// ===============================ALLOW CORS=======================================
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

// Seed admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DBconn>();

    // Check if admin user exists, if not create it
    if (dbContext.Users.FirstOrDefault(u => u.Email == "admin@example.com") == null)
    {
        var adminUser = new User
        {
            FirstName = "Leon",
            LastName = "M",
            Email = "admin@gmail.com",
            country = "USA",
            Password = "Admin@123", 
            Role = "Admin",
            DOB="DOB",
            Gender="Male"
        };

        dbContext.Users.Add(adminUser);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();
