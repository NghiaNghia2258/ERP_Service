using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BundleDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    MaxUsageCount = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolumeDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinQuantity = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<double>(type: "float", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BundleDiscountItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    BundleDiscountId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BundleDiscountItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BundleDiscountItem_BundleDiscounts_BundleDiscountId",
                        column: x => x.BundleDiscountId,
                        principalTable: "BundleDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BundleDiscountItem_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VolumeDiscountItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductVariantId = table.Column<int>(type: "int", nullable: false),
                    VolumeDiscountId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolumeDiscountItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolumeDiscountItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VolumeDiscountItems_VolumeDiscounts_VolumeDiscountId",
                        column: x => x.VolumeDiscountId,
                        principalTable: "VolumeDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 17, 19, 9, 27, 366, DateTimeKind.Local).AddTicks(2814));

            migrationBuilder.CreateIndex(
                name: "IX_BundleDiscountItem_BundleDiscountId",
                table: "BundleDiscountItem",
                column: "BundleDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_BundleDiscountItem_ProductVariantId",
                table: "BundleDiscountItem",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDiscountItems_ProductVariantId",
                table: "VolumeDiscountItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_VolumeDiscountItems_VolumeDiscountId",
                table: "VolumeDiscountItems",
                column: "VolumeDiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BundleDiscountItem");

            migrationBuilder.DropTable(
                name: "VolumeDiscountItems");

            migrationBuilder.DropTable(
                name: "BundleDiscounts");

            migrationBuilder.DropTable(
                name: "VolumeDiscounts");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2025, 5, 11, 23, 33, 46, 667, DateTimeKind.Local).AddTicks(9802));
        }
    }
}
