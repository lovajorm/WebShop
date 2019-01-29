using Microsoft.EntityFrameworkCore;
using WebShop.Bo;

namespace WebShop.Dal
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> context) : base(context) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductID = 1,
                    Title = "Produkt 1",
                    Description = "Beskrivning av produkt 1",
                    Price = 10,
                    ImageUrl = "Apelsin.png"
                },
                new Product()
                {
                    ProductID = 2,
                    Title = "Produkt 2",
                    Description = "Beskrivning av objekt 2",
                    Price = 20,
                    ImageUrl = "Apple.png"
                },
                new Product()
                {
                    ProductID = 3,
                    Title = "Produkt 3",
                    Description = "Beskrivning av objekt 3",
                    Price = 30,
                    ImageUrl = "Cassis.png"
                },
                new Product()
                {
                    ProductID = 4,
                    Title = "Produkt 4",
                    Description = "Beskrivning av objekt 4",
                    Price = 40,
                    ImageUrl = "CitronLine.png"
                },
                new Product()
                {
                    ProductID = 5,
                    Title = "Produkt 5",
                    Description = "Beskrivning av objekt 5",
                    Price = 50,
                    ImageUrl = "Hallon.png"
                },
                new Product()
                {
                    ProductID = 6,
                    Title = "Produkt 6",
                    Description = "Beskrivning av objekt 6",
                    Price = 60,
                    ImageUrl = "Paron.png"
                },
                new Product()
                {
                    ProductID = 7,
                    Title = "Produkt 7",
                    Description = "Beskrivning av objekt 7",
                    Price = 70,
                    ImageUrl = "Persika.png"
                },
                new Product()
                {
                    ProductID = 8,
                    Title = "Produkt 8",
                    Description = "Beskrivning av objekt 8",
                    Price = 80,
                    ImageUrl = "RodaBar.png"
                },
                new Product()
                {
                    ProductID = 9,
                    Title = "Produkt 9",
                    Description = "Beskrivning av objekt 9",
                    Price = 90,
                    ImageUrl = "Tropical.png"
                });
        }
    }
}
