using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;
using WebShop.Web.Interfaces;

namespace WebShop.Web.Mocks
{
    public class MockProductRepository:IProductRepository
    {
        private readonly ICategoryRepository _category = new MockCategoryRepository();

        public IEnumerable<Product> Products
        {
            get
            {
                return new List<Product>
                {
                    new Product
                    {
                        Title = "Sweater",
                        Description = "Beer is the world's oldest[1][2][3] and most widely consu",
                        Price = 50,
                        CategoryId = 1,
                        ImageUrl = "~/images/byx.jpg",
                    },
                    new Product
                    {
                        Title = "Pants",
                        Description = "Beer is the world's oldest[1][2][3] and most widely consu",
                        Price = 60,
                        CategoryId = 2,
                        ImageUrl = "~/images/byx.jpg",
                    }
                    
                };
            }
            set => throw new NotImplementedException();
        }

        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
