using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "UserEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "DeletedName", "IsDeleted", "Password", "RoleGroupId", "Username", "Version" },
                values: new object[,]
                {
                    { 3, null, null, null, false, "user123", 2, "user2", 0 },
                    { 4, null, null, null, false, "user123", 2, "user3", 0 },
                    { 5, null, null, null, false, "user123", 2, "user4", 0 },
                    { 6, null, null, null, false, "user123", 2, "user5", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "UserEvents");
        }
    }
}
