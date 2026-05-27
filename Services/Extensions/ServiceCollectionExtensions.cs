using DataAccess;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Services.Implementation;
using Services.Interfaces;

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

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IIngredientService, IngredientService>();

            return services;
        }

        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(CategoryProfile).Assembly);
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSettings = configuration.GetSection("Swagger");
            var darkMode = swaggerSettings.GetValue<bool>("DarkMode");

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Title = "RestaurantManager API",
                    Version = "v1"
                });

                if (darkMode)
                {
                    c.CustomSchemaIds(type => type.ToString());
                }
            });

            return services;
        }
    }
}
