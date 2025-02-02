using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class SeedDatabase
    {
        public static void Seed(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                SeedData(dbContext, webHostEnvironment.IsDevelopment());
            }
        }
        private static void SeedData(AppDbContext dbContext, bool isDevelopment)
        {
            if (!isDevelopment)
            {
                dbContext.Database.Migrate();
            }
            if (dbContext != null && !dbContext.Platforms.Any())
            {
                Console.WriteLine("Seeding Platform data");
                dbContext.Platforms.AddRange(new Platform()
                {
                    Name = "Dot Net",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "Sql Server Express",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform()
                {
                    Name = "Kubernetes",
                    Publisher = "Google",
                    Cost = "Free"
                }
                );
                dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Platform already have data"); ;
            }
        }
    }
}
