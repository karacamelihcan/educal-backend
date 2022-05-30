using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educal.Database.Migrations
{
    public partial class instructorlesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstructorLesson",
                columns: table => new
                {
                    InstructorsId = table.Column<int>(type: "integer", nullable: false),
                    LessonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorLesson", x => new { x.InstructorsId, x.LessonsId });
                    table.ForeignKey(
                        name: "FK_InstructorLesson_Instructors_InstructorsId",
                        column: x => x.InstructorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorLesson_Lessons_LessonsId",
                        column: x => x.LessonsId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorLesson_LessonsId",
                table: "InstructorLesson",
                column: "LessonsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorLesson");
        }
    }
}
