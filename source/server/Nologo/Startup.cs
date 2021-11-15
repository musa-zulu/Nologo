using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Serilog;
using System;
using System.IO;
using Nologo.Infrastructure.Extension;
using Nologo.Persistence;
using Nologo.Service;

namespace Nologo
{
    public class Startup
    {
        private readonly IConfigurationRoot configRoot;
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configRoot = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddController();

            services.AddDbContext(Configuration, configRoot);

            services.AddIdentityService(Configuration);

            services.AddAutoMapper();

            services.AddScopedServices();

            services.AddTransientServices();

            services.AddSwaggerOpenAPI();

            services.AddMailSetting(Configuration);

            services.AddServiceLayer();

            services.AddVersion();

            services.AddFeatureManagement();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
                 options.WithOrigins("http://localhost:3000")
                 .AllowAnyHeader()
                 .AllowAnyMethod());

            app.ConfigureCustomExceptionMiddleware();

            log.AddSerilog();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.ConfigureSwagger();

            RunMigrations(service);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RunMigrations(IServiceProvider service)
        {
            // This returns the context.
            using var context = service.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
