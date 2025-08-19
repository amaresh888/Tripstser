using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirbnbApi.Migrations
{
    /// <inheritdoc />
    public partial class imageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644616/pexels-kelly-2869215_lla3mr.jpg");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644760/pexels-boonkong-boonpeng-442952-1134176_stpwik.jpg");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644801/pexels-pixabay-261388_unh5uf.jpg");

            migrationBuilder.UpdateData(
                table: "hotels",
                keyColumn: "PropertyId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://res.cloudinary.com/dtxqeadyh/image/upload/v1754644858/pexels-thorsten-technoman-109353-338504_amp2hh.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
