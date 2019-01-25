﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Dal;

namespace WebShop.Dal.Migrations
{
    [DbContext(typeof(WebShopDbContext))]
    partial class WebShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Bo.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<float>("Price");

                    b.Property<string>("Title");

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new { ProductID = 1, Description = "Beskrivning av produkt 1", ImageUrl = "", Price = 10f, Title = "Produkt 1" },
                        new { ProductID = 2, Description = "Beskrivning av objekt 2", ImageUrl = "", Price = 20f, Title = "Produkt 2" },
                        new { ProductID = 3, Description = "Beskrivning av objekt 3", ImageUrl = "", Price = 30f, Title = "Produkt 3" },
                        new { ProductID = 4, Description = "Beskrivning av objekt 4", ImageUrl = "", Price = 40f, Title = "Produkt 4" },
                        new { ProductID = 5, Description = "Beskrivning av objekt 5", ImageUrl = "", Price = 50f, Title = "Produkt 5" },
                        new { ProductID = 6, Description = "Beskrivning av objekt 6", ImageUrl = "", Price = 60f, Title = "Produkt 6" },
                        new { ProductID = 7, Description = "Beskrivning av objekt 7", ImageUrl = "", Price = 70f, Title = "Produkt 7" },
                        new { ProductID = 8, Description = "Beskrivning av objekt 8", ImageUrl = "", Price = 80f, Title = "Produkt 8" },
                        new { ProductID = 9, Description = "Beskrivning av objekt 9", ImageUrl = "", Price = 90f, Title = "Produkt 9" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
