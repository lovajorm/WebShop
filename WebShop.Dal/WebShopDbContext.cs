using System;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;

namespace WebShop.Dal
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> context) : base(context) {}
    }

}
