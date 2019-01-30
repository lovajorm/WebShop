using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;
using WebShop.Web.Interfaces;

namespace WebShop.Web.Mocks
{
    public class MockCategoryRepository:ICategoryRepository
    {
        public IEnumerable<Category> Categories {
            get
            {
                return new List<Category>
                {
                    new Category { CategoryName = "Fruits", Description = "Fruits" },
                    new Category { CategoryName = "Electronics", Description = "Electronics" },
                };
            }
        }
    }
}
