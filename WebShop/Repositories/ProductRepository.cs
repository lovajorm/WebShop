using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Interfaces;

namespace WebShop.Web.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebShopDbContext _context;

        public ProductRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> Products
        {
            get => _context.Products.Include(c => c.Category);
        }

        public Product GetProductById(int productId) => _context.Products.FirstOrDefault(p => p.ProductID == productId);
    }
}
