using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "BrandName", "CategoryId", "CategoryName", "CreatedAt", "CreatedBy", "CreatedName", "DeletedAt", "DeletedBy", "DeletedName", "Description", "ImageUrls", "IsDeleted", "IsPhysicalProduct", "MainImageUrl", "Name", "NameEn", "OriginalPrice", "Price", "PropertyName1", "PropertyName2", "PropertyValue1", "PropertyValue2", "SellCount", "Specifications", "StoreId", "TotalInventory", "UnitWeight", "UpdatedAt", "UpdatedBy", "UpdatedName", "Version", "Weight" },
                values: new object[,]
                {
                    { 3, null, null, 1, null, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, null, "[\"https://placehold.co/100x100\"]", false, null, null, "Test - Đen 128GB", null, 100.0, 85.0, "Màu sắc", "Dung lượng", "Đen", "128GB", 0, null, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 0.0, null, null, null, null, 0, null },
                    { 4, null, null, 1, null, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, null, "[\"https://placehold.co/100x100\"]", false, null, null, "Test - Trắng 128GB", null, 100.0, 85.0, "Màu sắc", "Dung lượng", "Trắng", "128GB", 0, null, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 0.0, null, null, null, null, 0, null },
                    { 5, null, null, 1, null, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, null, "[\"https://placehold.co/100x100\"]", false, null, null, "Test - Đen 256GB", null, 120.0, 100.0, "Màu sắc", "Dung lượng", "Đen", "256GB", 0, null, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 0.0, null, null, null, null, 0, null },
                    { 6, null, null, 1, null, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, null, "[\"https://placehold.co/100x100\"]", false, null, null, "Test - Trắng 256GB", null, 120.0, 100.0, "Màu sắc", "Dung lượng", "Trắng", "256GB", 0, null, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 0.0, null, null, null, null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "CreatedName", "DeletedAt", "DeletedBy", "DeletedName", "ImageUrl", "Inventory", "IsActivate", "IsDeleted", "Price", "ProductId", "PropertyValue1", "PropertyValue2", "UpdatedAt", "UpdatedBy", "UpdatedName", "Version" },
                values: new object[,]
                {
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 85.0, 4, "Trắng", "128GB", null, null, null, 0 },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 100.0, 5, "Đen", "256GB", null, null, null, 0 },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 100.0, 6, "Trắng", "256GB", null, null, null, 0 },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "admin", null, null, null, "https://placehold.co/100x100", 10, true, false, 85.0, 3, "Đen", "128GB", null, null, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
