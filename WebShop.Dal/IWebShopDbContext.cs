using Microsoft.EntityFrameworkCore;
using WebShop.Bo;

namespace WebShop.Dal
{
    public interface IWebShopDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ShoppingCartItem> ShoppingCart { get; set; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        int SaveChanges();
        void Dispose();
        void Remove<T>(T entity) where T : class;
        void Add<T>(T entity) where T : class;
    }
}