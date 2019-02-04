using System.Collections;
using System.Collections.Generic;
using WebShop.Bo;

namespace WebShop.Web.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProductById(int productId);
    }
}