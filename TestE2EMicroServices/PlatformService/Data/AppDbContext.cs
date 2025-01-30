using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Platform>  Platforms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Platform>()
                .Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Platform>()
                .Property(e => e.Publisher).IsRequired();
            modelBuilder.Entity<Platform>()
                .Property(e => e.Cost).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
