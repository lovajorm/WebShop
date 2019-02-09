using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Log;
using WebShop.Web.Interfaces;
using WebShop.Web.UoW;
using WebShop.Web.ViewModels;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace WebShop.Web.Controllers
{
    public class ProductController : Controller
    {
        //private readonly WebShopDbContext _context;
        private UnitOfWork _context;
        private readonly IMessageLogger _logger;
        private readonly IMapper _mapper;
        //private readonly ICategoryRepository _categoryRepository;
        //private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository, WebShopDbContext context, IMessageLogger logger, IMapper mapper)
        {
            //_categoryRepository = categoryRepository;
            //_productRepository = productRepository;
            //_context = context;
            _context = new UnitOfWork(context);


            _logger = logger;
            _mapper = mapper;
        }

        public ViewResult List(string category)
        {
            //string _category = category;
            //IEnumerable<Product> products;
            //string currentCategory = string.Empty;

            //if (string.IsNullOrEmpty(category))
            //{
            //    products = _productRepository.Products.OrderBy(p => p.ProductID);
            //    currentCategory = "All products";
            //}
            //else
            //{
            //    if (string.Equals("Clothes", _category, StringComparison.OrdinalIgnoreCase))
            //        products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Clothes")).OrderBy(p => p.Category);
            //    else if 
            //        (string.Equals("Furniture", _category, StringComparison.OrdinalIgnoreCase))
            //        products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Furniture")).OrderBy(p => p.Category);
            //    else
            //        products = _productRepository.Products.Where(p => p.Category.CategoryName.Equals("Electronics")).OrderBy(p => p.Category);

            //    currentCategory = _category;
            //}

            //var productListViewModel = new ProductListViewModel
            //{
            //    Products = products,
            //    CurrentCategory = currentCategory
            //};

            IEnumerable<Product> products = null;
            if (string.IsNullOrEmpty(category))
            {
                products = _context.Product.GetProducts();
            }
            else
            {
                switch (category)
                {
                    case "Clothes":
                        products = _context.Product.Find(p => p.Category.CategoryName == category);
                        break;
                    case "Furniture":
                        products = _context.Product.Find(p => p.Category.CategoryName == category);
                        break;
                    case "Electronics":
                        products = _context.Product.Find(p => p.Category.CategoryName == category);
                        break;
                }
            }

            var productListViewModel = new ProductListViewModel
            {
                Products = products,
            };

            return View(productListViewModel);
        }

        // GET: WebShop/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Product.Find(p => p.ProductID == id).FirstOrDefault();
                //.FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}