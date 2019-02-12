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
        private readonly IWebShopDbContext _context;
        public IProductRepository Product { get; }
        public ShoppingCart ShoppingCart { get; set; }

        public UnitOfWork(IWebShopDbContext context, IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _context = context;
            Product = productRepository;
            ShoppingCart = shoppingCart;
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
