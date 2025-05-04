using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Teachers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Courses",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Teachers",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Courses",
                newName: "ImageUrl");
        }
    }
}
