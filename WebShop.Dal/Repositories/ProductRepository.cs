using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Dal.Repositories;
using WebShop.Web.Interfaces;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(WebShopDbContext context) : base(context) {}

        //public IIncludableQueryable<Product, Category> Products => context.Products.Include(c => c.Category);

        public WebShopDbContext WebShopDbContext => Context as WebShopDbContext;

        public IIncludableQueryable<Product, Category> GetProducts()
        {
            return WebShopDbContext.Products.Include(c => c.Category);
        }
    }
}
