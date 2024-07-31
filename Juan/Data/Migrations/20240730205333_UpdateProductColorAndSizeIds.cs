using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juan.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductColorAndSizeIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductColors");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // Otomatik artan olarak ekle

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1"); // Otomatik artan olarak ekle
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
