using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnWell.CourseManagement.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Additional_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "students",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_name",
                table: "students");
        }
    }
}
