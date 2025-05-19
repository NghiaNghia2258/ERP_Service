using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init129 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercent",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "OrderItems");

            migrationBuilder.AddColumn<bool>(
                name: "HasSelected",
                table: "OrderItems",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSelected",
                table: "OrderItems");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPercent",
                table: "OrderItems",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountValue",
                table: "OrderItems",
                type: "float",
                nullable: true);
        }
    }
}
