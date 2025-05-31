using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class E1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PaymentId",
                table: "Carts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipingAddressId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ShipingAddressId",
                table: "Carts");
        }
    }
}
