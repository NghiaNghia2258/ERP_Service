using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomer3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Version" },
                values: new object[,]
                {
                    { 5, "CREATE_CUSTOMER", 0 },
                    { 6, "DELETE_CUSTOMER", 0 },
                    { 7, "UPDATE_CUSTOMER", 0 },
                    { 8, "SELECT_CUSTOMER", 0 }
                });

            migrationBuilder.InsertData(
                table: "RoleRoleGroup",
                columns: new[] { "RoleGroupsId", "RolesId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 2, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
