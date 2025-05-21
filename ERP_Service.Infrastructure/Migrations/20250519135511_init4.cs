using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "CreatedName", "Debt", "DeletedAt", "DeletedBy", "DeletedName", "Email", "Gender", "IsActive", "IsDeleted", "Name", "Phone", "Point", "StoreId", "UserLoginId", "Version" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "CUST002", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Admin", 0.0, null, null, null, null, "Nữ", null, false, "Trần Thị B", "0909234567", 100, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 3, 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "CUST003", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Admin", 0.0, null, null, null, null, "Nam", null, false, "Lê Văn C", "0909345678", 80, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 4, 0 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "CUST004", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Admin", 0.0, null, null, null, null, "Nữ", null, false, "Phạm Thị D", "0909456789", 90, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 5, 0 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "CUST005", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", "Admin", 0.0, null, null, null, null, "Nam", null, false, "Đặng Văn E", "0909567890", 70, new Guid("bdbad2ae-0ffa-4420-acb9-275e5476013b"), 6, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));
        }
    }
}
