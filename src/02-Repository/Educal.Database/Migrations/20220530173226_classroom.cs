using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Educal.Database.Migrations
{
    public partial class classroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Students",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    InstructorId = table.Column<int>(type: "integer", nullable: true),
                    LessonId = table.Column<int>(type: "integer", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WeeklyHour = table.Column<int>(type: "integer", nullable: false),
                    TotalHour = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classrooms_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classrooms_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_InstructorId",
                table: "Classrooms",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_LessonId",
                table: "Classrooms",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassroomId",
                table: "Students",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassroomId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassroomId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Students");
        }
    }
}
