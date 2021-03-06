using BusinessLogicLayer.Base;
using BusinessLogicLayer.Services;
using Contracts;
using Contracts.Repository;
using LoggerService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;

namespace AkvelonDemoAPI.Extensions
{
    /// <summary>
    /// Class <c>ServiceExtensions</c>Configure all of our services
    /// </summary>
    public static class ServiceExtensions
    {
        //Adding Cors policy in our project
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        //Adding Logger service in our project
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        //Adding RepositoryManager service
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        //Adding ProjectService service
        public static void ConfigureProjectService(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
        }

        //Adding TaskService service
        public static void ConfigureTaskService(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}
