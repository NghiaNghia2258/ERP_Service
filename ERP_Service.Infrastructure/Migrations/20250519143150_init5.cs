using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BrandName", "CategoryId", "CategoryName", "CreatedAt", "CreatedBy", "CreatedName", "DeletedAt", "DeletedBy", "DeletedName", "Description", "ImageUrls", "IsDeleted", "IsPhysicalProduct", "MainImageUrl", "Name", "NameEn", "OriginalPrice", "Price", "PropertyName1", "PropertyName2", "PropertyValue1", "PropertyValue2", "SellCount", "Specifications", "StoreId", "TotalInventory", "UnitWeight", "UpdatedAt", "UpdatedBy", "UpdatedName", "Version", "Weight" },
                values: new object[] { 2, null, null, 1, null, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, null, "[\"https://placehold.co/100x100\",\"https://placehold.co/100x100\"]", false, null, null, "Test", null, 100.0, 85.0, "Màu sắc", "Dung lượng", "Đen,Trắng", "128GB", 0, null, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 0.0, null, null, null, null, 0, null });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedName", "DeletedAt", "DeletedBy", "DeletedName", "ImageUrl", "Inventory", "IsActivate", "IsDeleted", "Price", "ProductId", "PropertyValue1", "PropertyValue2", "UpdatedAt", "UpdatedBy", "UpdatedName", "Version" },
                values: new object[,]
                {
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 85.0, 2, "Đen", "128GB", null, null, null, 0 },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 85.0, 2, "Trắng", "128GB", null, null, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
