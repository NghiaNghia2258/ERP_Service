using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class B1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "InboundReceipts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InboundReceipts_StoreId",
                table: "InboundReceipts",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_InboundReceipts_Store_StoreId",
                table: "InboundReceipts",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Store_StoreId",
                table: "Orders",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboundReceipts_Store_StoreId",
                table: "InboundReceipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Store_StoreId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StoreId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_InboundReceipts_StoreId",
                table: "InboundReceipts");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "InboundReceipts");
        }
    }
}
