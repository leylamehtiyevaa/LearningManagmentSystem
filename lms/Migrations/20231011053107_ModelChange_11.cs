using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "InstructorId",
                table: "Material",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Course",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Material_InstructorId",
                table: "Material",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_AspNetUsers_InstructorId",
                table: "Material",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_AspNetUsers_InstructorId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_InstructorId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Course",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_InstructorId",
                table: "Course",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
