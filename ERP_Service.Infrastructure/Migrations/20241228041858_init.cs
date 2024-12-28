﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    RoleGroupId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DeletedName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_RoleGroups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalTable: "RoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRoleGroup",
                columns: table => new
                {
                    RoleGroupsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRoleGroup", x => new { x.RoleGroupsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RoleRoleGroup_RoleGroups_RoleGroupsId",
                        column: x => x.RoleGroupsId,
                        principalTable: "RoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleRoleGroup_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(13)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    Debt = table.Column<double>(type: "float", nullable: false),
                    UserLoginId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DeletedName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_UserLogins_UserLoginId",
                        column: x => x.UserLoginId,
                        principalTable: "UserLogins",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "Id", "Name", "Version" },
                values: new object[,]
                {
                    { 1, "Admin", 0 },
                    { 2, "User", 0 }
                });

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

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "DeletedAt", "DeletedBy", "DeletedName", "IsDeleted", "Password", "RoleGroupId", "Username", "Version" },
                values: new object[,]
                {
                    { 1, null, null, null, false, "admin123", 1, "admin", 0 },
                    { 2, null, null, null, false, "user123", 2, "user1", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserLoginId",
                table: "Customers",
                column: "UserLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRoleGroup_RolesId",
                table: "RoleRoleGroup",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_RoleGroupId",
                table: "UserLogins",
                column: "RoleGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "RoleRoleGroup");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "RoleGroups");
        }
    }
}
