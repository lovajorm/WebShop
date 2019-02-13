using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
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
