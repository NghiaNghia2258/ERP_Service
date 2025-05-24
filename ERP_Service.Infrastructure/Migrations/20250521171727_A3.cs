using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class A3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "CartItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_StoreId",
                table: "CartItem",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Store_StoreId",
                table: "CartItem",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Store_StoreId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_StoreId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CartItem");
        }
    }
}
