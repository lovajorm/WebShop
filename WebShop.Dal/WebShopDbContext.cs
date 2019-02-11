﻿using Microsoft.EntityFrameworkCore;
using WebShop.Bo;

namespace WebShop.Dal
{
    public class WebShopDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }   //virtual can be overridden in derived class eg. overridden by any class that inherits it.
        public virtual DbSet<ShoppingCartItem> ShoppingCart { get; set; }
        //public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail > OrderDetails { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> context) : base(context) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Clothes",
                    Description = "Clothes",
                },
                new Category()
                {
                    CategoryId = 2,
                    CategoryName = "Furniture",
                    Description = "Furniture",
                },
                new Category()
                {
                    CategoryId = 3,
                    CategoryName = "Electronics",
                    Description = "Electronics",
                });


            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductID = 1,
                    Title = "Sweater",
                    Description = "Knitted mens sweater",
                    Price = 100,
                    ImageUrl = "~/images/Clothes/svart.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 2,
                    Title = "Pants",
                    Description = "Black womens pants",
                    Price = 200,
                    ImageUrl = "~/images/Clothes/byx.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 3,
                    Title = "Hoodie",
                    Description = "Black hoodie",
                    Price = 359,
                    ImageUrl = "~/images/Clothes/hoodie.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 4,
                    Title = "Skirt",
                    Description = "Leopard skirt",
                    Price = 349,
                    ImageUrl = "~/images/Clothes/kjol.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 5,
                    Title = "Cardigan",
                    Description = "Grey cardigan",
                    Price = 500,
                    ImageUrl = "~/images/Clothes/kofta.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 6,
                    Title = "Jeans",
                    Description = "Blue jeans",
                    Price = 599,
                    ImageUrl = "~/images/Clothes/jeans.jfif",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 7,
                    Title = "T-shirt",
                    Description = "Mens T-shirt",
                    Price = 99,
                    ImageUrl = "~/images/Clothes/tshirt.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 8,
                    Title = "Blouse",
                    Description = "Womens blouse",
                    Price = 449,
                    ImageUrl = "~/images/Clothes/blus.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 9,
                    Title = "Shorts",
                    Description = "Blue shorts",
                    Price = 249,
                    ImageUrl = "~/images/Clothes/shorts.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductID = 10,
                    Title = "Laptop",
                    Description = "Laptop from HP",
                    Price = 10000,
                    ImageUrl = "~/images/Electronics/laptop.jfif",
                    CategoryId = 3
                },
                new Product()
                {
                    ProductID = 11,
                    Title = "Table",
                    Description = "Dinner table",
                    Price = 2000,
                    ImageUrl = "~/images/Furniture/table.jpg",
                    CategoryId = 2
                });
        }
    }
}
