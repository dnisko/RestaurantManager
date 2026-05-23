using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(connectionString)
                    //.EnableSensitiveDataLogging() // This enables detailed logging of SQL queries
                    .LogTo(Console.WriteLine, LogLevel.Information)); //,
            //ServiceLifetime.Scoped);

            return services;
        }
        public static void EnsureSeeded(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RestaurantDbContext>();

            context.Database.Migrate();

            if (context.RestaurantTables.Any() && context.Categories.Any() && context.Products.Any())
            {
                Console.WriteLine("➡️ Database already seeded.");
            }
            else
            {
                Console.WriteLine("➡️ Seeding database...");
                DataSeeder.SeedData(context); //DataSeeder needs to be implemented to seed the database with initial data
            }
        }
    }
}
