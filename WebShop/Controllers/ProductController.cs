using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using WebShop.Web.Repositories;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebShopDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, WebShopDbContext context)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _context = context;
        }

        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Product> products;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                products = _productRepository.Products.OrderBy(p => p.ProductID);
                currentCategory = "All products";
            }
            else
            {
                if (string.Equals("Clothes", _category, StringComparison.OrdinalIgnoreCase))
                    products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Clothes")).OrderBy(p => p.Category);
                else if 
                    (string.Equals("Furniture", _category, StringComparison.OrdinalIgnoreCase))
                    products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Furniture")).OrderBy(p => p.Category);
                else
                    products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Electronics")).OrderBy(p => p.Category);

                currentCategory = _category;
            }

            var productListViewModel = new ProductListViewModel
            {
                Products = products,
                CurrentCategory = currentCategory
            };
            return View(productListViewModel);
        }

        // GET: WebShop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}