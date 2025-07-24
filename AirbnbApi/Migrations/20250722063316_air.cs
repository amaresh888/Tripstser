﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirbnbApi.Migrations
{
    /// <inheritdoc />
    public partial class air : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "users",
                newName: "Id");
        }
    }
}
