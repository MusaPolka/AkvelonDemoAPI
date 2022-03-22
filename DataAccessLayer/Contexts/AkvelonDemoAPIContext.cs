using DataAccessLayer.Configs;
using DataAccessLayer.Extensions;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contexts
{
    public class AkvelonDemoAPIContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public AkvelonDemoAPIContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfig());
            modelBuilder.ApplyConfiguration(new TaskModelConfig());
            modelBuilder.ApplyConfiguration(new ProjectSeeder());
            modelBuilder.ApplyConfiguration(new TaskSeeder());
        }
    }
}
