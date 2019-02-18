﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public IWebShopDbContext WebShopDbContext => Context as IWebShopDbContext;

        public ProductRepository(IWebShopDbContext context) : base(context) {}

        public IIncludableQueryable<Product, Category> GetProducts()
        {
            return WebShopDbContext.Products.Include(c => c.Category);
        }

    }
}
