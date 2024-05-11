using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "iconURL",
                table: "Category",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "iconURL",
                table: "Category");
        }
    }
}
