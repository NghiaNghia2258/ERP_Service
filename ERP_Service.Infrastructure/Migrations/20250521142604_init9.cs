using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "Name", "Version" },
                values: new object[] { 3, "OwnerStore", 0 });

            migrationBuilder.InsertData(
                table: "RoleRoleGroup",
                columns: new[] { "RoleGroupsId", "RolesId" },
                values: new object[,]
                {
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 }
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "DeletedName", "IsDeleted", "Password", "RoleGroupId", "Username", "Version" },
                values: new object[] { 7, null, null, null, false, "store123", 3, "store1", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "RoleRoleGroup",
                keyColumns: new[] { "RoleGroupsId", "RolesId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "UserLogins",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoleGroups",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
