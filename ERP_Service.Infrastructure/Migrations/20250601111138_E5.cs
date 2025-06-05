using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class E5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Vouchers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_StoreId",
                table: "Vouchers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Store_StoreId",
                table: "Vouchers",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Store_StoreId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_StoreId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Vouchers");
        }
    }
}
