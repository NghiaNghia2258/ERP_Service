using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApplyForAll",
                table: "VolumeDiscounts",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExcludeVolumeDiscountItems",
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
                    table.PrimaryKey("PK_ExcludeVolumeDiscountItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcludeVolumeDiscountItems_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExcludeVolumeDiscountItems_VolumeDiscounts_VolumeDiscountId",
                        column: x => x.VolumeDiscountId,
                        principalTable: "VolumeDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcludeVolumeDiscountItems_ProductVariantId",
                table: "ExcludeVolumeDiscountItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcludeVolumeDiscountItems_VolumeDiscountId",
                table: "ExcludeVolumeDiscountItems",
                column: "VolumeDiscountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcludeVolumeDiscountItems");

            migrationBuilder.DropColumn(
                name: "IsApplyForAll",
                table: "VolumeDiscounts");
        }
    }
}
