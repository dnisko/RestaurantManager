
using Common.Settings;
using Serilog;
using Services.Extensions;

namespace RestaurantManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var appConfig = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appConfig);
            var appSettings = appConfig.Get<AppSettings>();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });
            builder.Services.AddOpenApi();
                
            builder.Services.RegisterAutoMapper();

            builder.Services.AddHttpClient();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(
                        path: $"Logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                        retainedFileCountLimit: 7
                    )
                    .Enrich.FromLogContext();
            });

            if (appSettings != null) builder.Services.RegisterDbContext(appSettings.ConnectionString);

            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();
            builder.WebHost.ConfigureKestrel(opts =>
            {
                opts.ListenAnyIP(7175);
                opts.ListenAnyIP(5015);
            });

            var swaggerSettings = builder.Configuration.GetSection("Swagger");
            var darkMode = swaggerSettings.GetValue<bool>("DarkMode");
            builder.Services.AddSwagger(builder.Configuration);

            builder.Services.RegisterCors();

            var app = builder.Build();
            app.Services.EnsureSeeded();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseStaticFiles();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    var swaggerSettings = builder.Configuration.GetSection("Swagger");
                    var darkMode = swaggerSettings.GetValue<bool>("DarkMode");
                    Console.WriteLine($"Dark Mode: {darkMode}");
                    if (darkMode)
                    {
                        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                    }
                });
            }

            app.UseCors("AllowAll");

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
