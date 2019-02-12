using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        int Complete();
        void Dispose();
        void Remove<T>(T entity) where T : class;
        void Add<T>(T entity) where T : class;
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        T Get<T>(int id) where T : class;
    }
}