using DataAccessLayer.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions
{
    public static class DbContextConfigurator
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AkvelonDemoAPIContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }   
    }
}
