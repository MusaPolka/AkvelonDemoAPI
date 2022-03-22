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
    public class TaskSeeder : IEntityTypeConfiguration<TaskModel>
    {
        public void Configure(EntityTypeBuilder<TaskModel> builder)
        {
            builder.HasData
            (
                new TaskModel
                {
                    Id = 1,
                    Name = "Database Creation",
                    Description = "Create initial databse and models",
                    Priority = Enums.Priority.Done,
                    ProjectId = 1
                },
                new TaskModel
                {
                    Id = 2,
                    Name = "CRUD Operations",
                    Description = "Add Create, Read, Update and Delete functionality",
                    Priority = Enums.Priority.InProgress,
                    ProjectId = 1
                },
                new TaskModel
                {
                    Id = 3,
                    Name = "Designing Architecture",
                    Description = "Design microservice architecture using modern technologies",
                    Priority = Enums.Priority.Todo,
                    ProjectId = 2
                },
                new TaskModel
                {
                    Id = 4,
                    Name = "Designing Infrastructure",
                    Description = "Design and create fault tolerant infrastructure",
                    Priority = Enums.Priority.Todo,
                    ProjectId = 2
                }
            );
        }
    }
}
