using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educal.Database.Migrations
{
    public partial class districtbug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Districts_Name",
                table: "Districts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Districts_Name",
                table: "Districts",
                column: "Name",
                unique: true);
        }
    }
}
