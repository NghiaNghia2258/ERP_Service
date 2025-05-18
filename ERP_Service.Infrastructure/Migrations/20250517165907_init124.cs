using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "VolumeDiscounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "VolumeDiscounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "BundleDiscounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "BundleDiscounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "VolumeDiscounts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "VolumeDiscounts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "BundleDiscounts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BundleDiscounts");
        }
    }
}
