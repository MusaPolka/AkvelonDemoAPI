using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskModelId",
                table: "Tasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Projects",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CompletedAt", "Name", "StartedAt", "Status" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AkvelonAPI", new DateTime(2022, 3, 22, 15, 15, 47, 993, DateTimeKind.Local).AddTicks(7932), 1 });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CompletedAt", "Name", "StartedAt", "Status" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CarCity", new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "Name", "Priority", "ProjectId" },
                values: new object[,]
                {
                    { 1, "Create initial databse and models", "Database Creation", 2, 1 },
                    { 2, "Add Create, Read, Update and Delete functionality", "CRUD Operations", 1, 1 },
                    { 3, "Design microservice architecture using modern technologies", "Designing Architecture", 0, 2 },
                    { 4, "Design and create fault tolerant infrastructure", "Designing Infrastructure", 0, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "TaskModelId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Projects",
                newName: "ProjectId");
        }
    }
}
