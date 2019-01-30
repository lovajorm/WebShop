using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Bo;
using WebShop.Dal;

namespace WebShop.Web.Models
{
    public class DbInitializer
    {
        public static void Seed(IServiceProvider applicationBuilder)
        {
            WebShopDbContext context = applicationBuilder.GetRequiredService<WebShopDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Products.Any())
            {
                context.AddRange
                (
                    new Product
                    {
                        Title = "Sweater",
                        Price = 50,
                        Description = "Comfy sweater",
                        Category = Categories["Clothes"],
                        ImageUrl = "~/images/svart.jpg",
                        CategoryId = 1
                    },
                    new Product
                    {
                        Title = "Pants",
                        Price = 700,
                        Description = "Black pants",
                        Category = Categories["Clothes"],
                        ImageUrl = "~/images/byx.jpg",
                        CategoryId = 1
                    },
                    new Product
                    {
                        Title = "Hoodie",
                        Price = 500,
                        Description = "Comfy hoodie",
                        Category = Categories["Clothes"],
                        ImageUrl = "~/images/hoodie.jpg",
                        CategoryId = 1
                    }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { CategoryName = "Clothes", Description="Clothes" },
                        new Category { CategoryName = "Electronics", Description="Electronics" }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.CategoryName, genre);
                    }
                }

                return categories;
            }
        }
    }
}