using Microsoft.EntityFrameworkCore.Query;
using WebShop.Bo;

namespace WebShop.Dal.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IIncludableQueryable<Product, Category> GetProducts();
    }
}