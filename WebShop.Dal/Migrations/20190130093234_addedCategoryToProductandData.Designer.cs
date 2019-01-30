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
    [Migration("20190130093234_addedCategoryToProductandData")]
    partial class addedCategoryToProductandData
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
                        new { CategoryId = 2, CategoryName = "Furniture", Description = "Furniture" }
                    );
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
                        new { ProductID = 1, CategoryId = 1, Description = "Beskrivning av produkt 1", ImageUrl = "~/images/svart.jpg", Price = 10f, Title = "Produkt 1" },
                        new { ProductID = 2, CategoryId = 1, Description = "Beskrivning av objekt 2", ImageUrl = "~/images/byx.jpg", Price = 20f, Title = "Produkt 2" },
                        new { ProductID = 3, CategoryId = 2, Description = "Beskrivning av objekt 3", ImageUrl = "~/images/svart.jpg", Price = 30f, Title = "Produkt 3" },
                        new { ProductID = 4, CategoryId = 2, Description = "Beskrivning av objekt 4", ImageUrl = "~/images/byx.jpg", Price = 40f, Title = "Produkt 4" },
                        new { ProductID = 5, CategoryId = 1, Description = "Beskrivning av objekt 5", ImageUrl = "~/images/hoodie.jpg", Price = 50f, Title = "Produkt 5" },
                        new { ProductID = 6, CategoryId = 1, Description = "Beskrivning av objekt 6", ImageUrl = "~/images/svart.jpg", Price = 60f, Title = "Produkt 6" },
                        new { ProductID = 7, CategoryId = 2, Description = "Beskrivning av objekt 7", ImageUrl = "~/images/hoodie.jpg", Price = 70f, Title = "Produkt 7" },
                        new { ProductID = 8, CategoryId = 1, Description = "Beskrivning av objekt 8", ImageUrl = "~/images/hoodie.jpg", Price = 80f, Title = "Produkt 8" },
                        new { ProductID = 9, CategoryId = 1, Description = "Beskrivning av objekt 9", ImageUrl = "~/images/byx.jpg", Price = 90f, Title = "Produkt 9" }
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

                    b.ToTable("ShoppingCartItems");
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
