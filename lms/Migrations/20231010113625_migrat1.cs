using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms.Migrations
{
    /// <inheritdoc />
    public partial class migrat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "step",
                table: "Enrollment",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "step",
                table: "Enrollment");
        }
    }
}
