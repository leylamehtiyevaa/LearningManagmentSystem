using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_IdentityUserId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_IdentityUserId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Enrollment");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_UserId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Enrollment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_IdentityUserId",
                table: "Enrollment",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_IdentityUserId",
                table: "Enrollment",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
