using Backed.Models;
using Microsoft.EntityFrameworkCore;

namespace DBconnection_namespace
{
    public class DBconn : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSubscribers> EventSubscribers { get; set; }

        public DBconn(DbContextOptions<DBconn> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationships
            modelBuilder.Entity<Gallery>()
                .HasOne(g => g.User)
                .WithMany(u => u.Galleries)
                .HasForeignKey(g => g.UserId);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Gallery)
                .WithMany(g => g.Comments)
                .HasForeignKey(c => c.PictureId);

            modelBuilder.Entity<EventSubscribers>()
                .HasOne(es => es.User)
                .WithMany(u => u.EventSubscribers)
                .HasForeignKey(es => es.UserId);

            modelBuilder.Entity<EventSubscribers>()
                .HasOne(es => es.Event)
                .WithMany(e => e.EventSubscribers)
                .HasForeignKey(es => es.EventId);
        }
    }
}
