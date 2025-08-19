using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirbnbApi.Migrations
{
    /// <inheritdoc />
    public partial class Fianl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.pexels.com/photo/beige-concrete-building-70441/");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.pexels.com/photo/brown-hotel-signage-96444/");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.pexels.com/photo/red-and-brown-floral-stair-carpet-161758/");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://www.pexels.com/photo/white-sofas-inside-building-2507010/");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "hotels");
        }
    }
}
