using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educal.Database.Migrations
{
    public partial class classroomedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Classrooms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Classrooms",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalWeek",
                table: "Classrooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Classrooms");

            migrationBuilder.DropColumn(
                name: "TotalWeek",
                table: "Classrooms");
        }
    }
}
