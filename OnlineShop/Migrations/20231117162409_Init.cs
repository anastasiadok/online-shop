using System;
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineShop.Data.Models;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:status_type", "in_review,in_delivery,completed,cancelled")
                .Annotation("Npgsql:Enum:user_type", "admin,user");

            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("brands_pkey", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    color_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("colors_pkey", x => x.color_id);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    section_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sections_pkey", x => x.section_id);
                });

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    size_id = table.Column<Guid>(type: "uuid", nullable: false),
                    size_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sizes_pkey", x => x.size_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<UserType>(type: "user_type", nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.category_id);
                    table.ForeignKey(
                        name: "parentcategory_categories_id_fkey",
                        column: x => x.ParentCategoryId,
                        principalTable: "categories",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "section_categories_id_fkey",
                        column: x => x.SectionId,
                        principalTable: "sections",
                        principalColumn: "section_id");
                });

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("adresses_pkey", x => x.address_id);
                    table.ForeignKey(
                        name: "adresses_user_id_fkey",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    average_rating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.product_id);
                    table.ForeignKey(
                        name: "products_brand_id_fkey",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "brand_id");
                    table.ForeignKey(
                        name: "products_category_id_fkey",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    total_price = table.Column<decimal>(type: "money", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp(6) with time zone", precision: 6, nullable: false),
                    status = table.Column<TransactionStatus>(type: "status_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey", x => x.order_id);
                    table.ForeignKey(
                        name: "orders_adress_id_fkey",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "orders_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    medium_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    file_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    file_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("media_pkey", x => x.medium_id);
                    table.ForeignKey(
                        name: "media_product_id_fkey",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "product_variants",
                columns: table => new
                {
                    prod_variant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_id = table.Column<Guid>(type: "uuid", nullable: false),
                    size_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    sku = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_variants_pkey", x => x.prod_variant_id);
                    table.ForeignKey(
                        name: "product_variants_color_id_fkey",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "color_id");
                    table.ForeignKey(
                        name: "product_variants_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "product_variants_size_id_fkey",
                        column: x => x.size_id,
                        principalTable: "sizes",
                        principalColumn: "size_id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment_text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(6) with time zone", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reviews_pkey", x => x.review_id);
                    table.ForeignKey(
                        name: "reviews_product_id_fkey",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "reviews_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "order_transactions",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<TransactionStatus>(type: "status_type", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(6) with time zone", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_tr", x => new { x.order_id, x.status });
                    table.ForeignKey(
                        name: "order_transactions_transaction_id_fkey",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    product_var_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_item", x => new { x.user_id, x.product_var_id });
                    table.ForeignKey(
                        name: "cart_items_product_var_id_fkey",
                        column: x => x.product_var_id,
                        principalTable: "product_variants",
                        principalColumn: "prod_variant_id");
                    table.ForeignKey(
                        name: "cart_items_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    product_var_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => new { x.product_var_id, x.order_id });
                    table.ForeignKey(
                        name: "order_items_order_id_fkey",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "order_items_product_var_id_fkey",
                        column: x => x.product_var_id,
                        principalTable: "product_variants",
                        principalColumn: "prod_variant_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserId",
                table: "addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "brands_name_key",
                table: "brands",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_product_var_id",
                table: "cart_items",
                column: "product_var_id");

            migrationBuilder.CreateIndex(
                name: "categories_name_sections_key",
                table: "categories",
                columns: new[] { "name", "SectionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentCategoryId",
                table: "categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_SectionId",
                table: "categories",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "colors_color_name_key",
                table: "colors",
                column: "color_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_media_ProductId",
                table: "media",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "media_file_type_file_name_key",
                table: "media",
                columns: new[] { "file_type", "file_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "by_order_status",
                table: "order_transactions",
                columns: new[] { "order_id", "status" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_address_id",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_color_id",
                table: "product_variants",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_product_id",
                table: "product_variants",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_variants_size_id",
                table: "product_variants",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "product_variants_sku_key",
                table: "product_variants",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "prod_by_brand_category",
                table: "products",
                columns: new[] { "brand_id", "category_id" });

            migrationBuilder.CreateIndex(
                name: "products_name_key",
                table: "products",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "by_product_rating",
                table: "reviews",
                columns: new[] { "product_id", "rating" });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "sections_name_key",
                table: "sections",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "sizes_size_name_key",
                table: "sizes",
                column: "size_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_emai_phone_key",
                table: "users",
                columns: new[] { "email", "phone" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "order_transactions");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "product_variants");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "sizes");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "sections");
        }
    }
}
