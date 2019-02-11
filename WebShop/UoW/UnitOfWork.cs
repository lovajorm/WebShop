using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;
using WebShop.Web.Repositories;

namespace WebShop.Web.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebShopDbContext _context;
        public IProductRepository Product { get; }
        public IOrderRepository Order { get; }
        public ShoppingCart ShoppingCart { get; set; }

        public UnitOfWork(WebShopDbContext context)
        {
            _context = context;
            Product = new ProductRepository(context);
            Order = new OrderRepository(context);
            ShoppingCart = new ShoppingCart(context);
        }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
