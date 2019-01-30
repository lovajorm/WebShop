using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Interfaces;

namespace WebShop.Web.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebShopDbContext _context;

        public CategoryRepository(WebShopDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categories;
    }
}
