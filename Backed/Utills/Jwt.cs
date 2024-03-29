using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Backed.Utills
{
    public class Jwt
    {
  
		private readonly IConfiguration _configuration;

		public Jwt(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		internal string? GenerateToken(User user)
		{
			string issuer = _configuration.GetSection("JwtOptions:issuer").Value;
			string secretKey = _configuration.GetSection("JwtOptions:secrete_Key").Value;
			string audience = _configuration.GetSection("JwtOptions:Audience").Value;

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			// Claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.FirstName),
				/*new Claim(ClaimTypes.Role, user.),*/
				new Claim("user_id", $"{user.Id}"),
				new Claim("username",$"{user.Email}"),
				new Claim("user_email",$"{user.Email}"),
				new Claim("user_role",$"{user.Role}"),
				new Claim("fullname",$"{user.FirstName}"),
				// new Claim("profile_url",$"{user.ProfileUrl}"),

			};

			var token = new JwtSecurityToken(
				issuer,           // Issuer
				audience,         // Audience
				claims,           // Claims
				DateTime.Now,
				DateTime.Now.AddMinutes(12230),  // Expiry
				credentials        // Signing credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		internal string? GetUsernameFromToken(string jwtToken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.ReadToken(processToken(jwtToken) ) as JwtSecurityToken;
 
			var usernameClaim = token?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

			return usernameClaim?.Value;
		}

		internal string? GetUserIdFromToken(string jwtToken)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.ReadToken(processToken(jwtToken)) as JwtSecurityToken;

			var userId = token?.Claims.FirstOrDefault(claim => claim.Type == "user_id");

			return userId?.Value;
		}

		internal string? processToken(String rawToken)
		{
			if(rawToken!=null)
			{
				String token = rawToken.Split(" ")[1];
				return token;
			}
			return null;

		}


	}
    }