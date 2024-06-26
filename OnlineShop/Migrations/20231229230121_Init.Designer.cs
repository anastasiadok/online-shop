﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Data;

#nullable disable

namespace OnlineShop.Migrations
{
    [DbContext(typeof(OnlineshopContext))]
    [Migration("20231229230121_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "order_status", new[] { "in_review", "in_delivery", "completed", "cancelled" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "user_type", new[] { "user", "admin" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Data.Models.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Brand", b =>
                {
                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("BrandId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.CartItem", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "ProductVariantId");

                    b.HasIndex("ProductVariantId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid?>("ParentCategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("SectionId");

                    b.HasIndex("Name", "SectionId")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Color", b =>
                {
                    b.Property<Guid>("ColorId")
                        .HasColumnType("uuid");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("ColorId");

                    b.HasIndex("ColorName")
                        .IsUnique();

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Media", b =>
                {
                    b.Property<Guid>("MediaId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("MediaId");

                    b.HasIndex("ProductId");

                    b.HasIndex("FileType", "FileName")
                        .IsUnique();

                    b.ToTable("Media");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("OrderId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Data.Models.OrderItem", b =>
                {
                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ProductVariantId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.OrderTransaction", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("OrderId", "Status");

                    b.HasIndex("OrderId", "Status")
                        .IsUnique();

                    b.ToTable("OrderTransactions");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<float?>("AverageRating")
                        .HasColumnType("real");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("BrandId", "CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.ProductVariant", b =>
                {
                    b.Property<Guid>("ProductVariantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("SizeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("ProductVariantId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Review", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid");

                    b.Property<string>("CommentText")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("ReviewId");

                    b.HasIndex("UserId");

                    b.HasIndex("ProductId", "Rating");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Section", b =>
                {
                    b.Property<Guid>("SectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("SectionId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Size", b =>
                {
                    b.Property<Guid>("SizeId")
                        .HasColumnType("uuid");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("SizeId");

                    b.HasIndex("SizeName")
                        .IsUnique();

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("Email", "Phone")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Address", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.ProductVariant", "ProductVariant")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductVariantId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.User", "User")
                        .WithMany("CartItems")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("ProductVariant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Category", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Category", "ParentCategory")
                        .WithMany("Categories")
                        .HasForeignKey("ParentCategoryId");

                    b.HasOne("OnlineShop.Data.Models.Section", "Section")
                        .WithMany("Categories")
                        .HasForeignKey("SectionId")
                        .IsRequired();

                    b.Navigation("ParentCategory");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Media", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Product", "Product")
                        .WithMany("Media")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Address", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.OrderItem", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.ProductVariant", "ProductVariant")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductVariantId")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.OrderTransaction", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Product", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.ProductVariant", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Color", "Color")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ColorId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.Product", "Product")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.Size", "Size")
                        .WithMany("ProductVariants")
                        .HasForeignKey("SizeId")
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Review", b =>
                {
                    b.HasOne("OnlineShop.Data.Models.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.HasOne("OnlineShop.Data.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Address", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Category", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Color", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Product", b =>
                {
                    b.Navigation("Media");

                    b.Navigation("ProductVariants");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.ProductVariant", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Section", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.Size", b =>
                {
                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("OnlineShop.Data.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("CartItems");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
