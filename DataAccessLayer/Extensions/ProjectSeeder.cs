using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Extensions
{
    public class ProjectSeeder : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasData
            (
                new Project
                {
                    Id = 1,
                    Name = "AkvelonAPI",
                    StartedAt = DateTime.Now,
                    Status = Enums.Status.Active,
                    
                },
                new Project
                {
                    Id = 2,
                    Name = "CarCity",
                    StartedAt = new DateTime(2022, 4, 1),
                    Status = Enums.Status.NotStarted,
                }
            ); 
        }
    }
}
