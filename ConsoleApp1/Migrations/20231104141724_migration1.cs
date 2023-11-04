using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp1.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "products_category_id_fkey",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "products",
                newName: "subcategory_id");

            migrationBuilder.RenameIndex(
                name: "prod_by_brand_category",
                table: "products",
                newName: "prod_by_brand_subcategory");

            migrationBuilder.RenameIndex(
                name: "IX_products_category_id",
                table: "products",
                newName: "IX_products_subcategory_id");

            migrationBuilder.AddForeignKey(
                name: "products_subcategory_id_fkey",
                table: "products",
                column: "subcategory_id",
                principalTable: "subcategories",
                principalColumn: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "products_subcategory_id_fkey",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "subcategory_id",
                table: "products",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "prod_by_brand_subcategory",
                table: "products",
                newName: "prod_by_brand_category");

            migrationBuilder.RenameIndex(
                name: "IX_products_subcategory_id",
                table: "products",
                newName: "IX_products_category_id");

            migrationBuilder.AddForeignKey(
                name: "products_category_id_fkey",
                table: "products",
                column: "category_id",
                principalTable: "subcategories",
                principalColumn: "category_id");
        }
    }
}
