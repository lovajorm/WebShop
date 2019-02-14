﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Dal;

namespace WebShop.Dal.Migrations
{
    [DbContext(typeof(WebShopDbContext))]
    [Migration("20190213180913_addedShoppingCartItem_again")]
    partial class addedShoppingCartItem_again
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Bo.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new { CategoryId = 1, CategoryName = "Clothes", Description = "Clothes" },
                        new { CategoryId = 2, CategoryName = "Furniture", Description = "Furniture" },
                        new { CategoryId = 3, CategoryName = "Electronics", Description = "Electronics" }
                    );
                });

            modelBuilder.Entity("WebShop.Bo.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Address2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(20);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderPlaced");

                    b.Property<float>("OrderTotal");

                    b.Property<string>("Ssn");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebShop.Bo.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int>("OrderId");

                    b.Property<float>("Price");

                    b.Property<int>("ProductID");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebShop.Bo.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<float>("Price");

                    b.Property<string>("Title");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new { ProductID = 1, CategoryId = 1, Description = "Knitted mens sweater", ImageUrl = "~/images/Clothes/svart.jpg", Price = 100f, Title = "Sweater" },
                        new { ProductID = 2, CategoryId = 1, Description = "Black womens pants", ImageUrl = "~/images/Clothes/byx.jpg", Price = 200f, Title = "Pants" },
                        new { ProductID = 3, CategoryId = 1, Description = "Black hoodie", ImageUrl = "~/images/Clothes/hoodie.jpg", Price = 359f, Title = "Hoodie" },
                        new { ProductID = 4, CategoryId = 1, Description = "Leopard skirt", ImageUrl = "~/images/Clothes/kjol.jpg", Price = 349f, Title = "Skirt" },
                        new { ProductID = 5, CategoryId = 1, Description = "Grey cardigan", ImageUrl = "~/images/Clothes/kofta.jpg", Price = 500f, Title = "Cardigan" },
                        new { ProductID = 6, CategoryId = 1, Description = "Blue jeans", ImageUrl = "~/images/Clothes/jeans.jfif", Price = 599f, Title = "Jeans" },
                        new { ProductID = 7, CategoryId = 1, Description = "Mens T-shirt", ImageUrl = "~/images/Clothes/tshirt.jpg", Price = 99f, Title = "T-shirt" },
                        new { ProductID = 8, CategoryId = 1, Description = "Womens blouse", ImageUrl = "~/images/Clothes/blus.jpg", Price = 449f, Title = "Blouse" },
                        new { ProductID = 9, CategoryId = 1, Description = "Blue shorts", ImageUrl = "~/images/Clothes/shorts.jpg", Price = 249f, Title = "Shorts" },
                        new { ProductID = 10, CategoryId = 3, Description = "Laptop from HP", ImageUrl = "~/images/Electronics/laptop.jfif", Price = 10000f, Title = "Laptop" },
                        new { ProductID = 11, CategoryId = 2, Description = "Dinner table", ImageUrl = "~/images/Furniture/table.jpg", Price = 2000f, Title = "Table" }
                    );
                });

            modelBuilder.Entity("WebShop.Bo.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int?>("ProductID");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("ProductID");

                    b.ToTable("ShoppingCartItem");
                });

            modelBuilder.Entity("WebShop.Bo.OrderDetail", b =>
                {
                    b.HasOne("WebShop.Bo.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebShop.Bo.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebShop.Bo.Product", b =>
                {
                    b.HasOne("WebShop.Bo.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebShop.Bo.ShoppingCartItem", b =>
                {
                    b.HasOne("WebShop.Bo.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });
#pragma warning restore 612, 618
        }
    }
}