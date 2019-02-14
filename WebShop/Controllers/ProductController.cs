using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Dal.Interfaces;
using WebShop.Dal.UoW;
using WebShop.Log;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMessageLogger _logger;
        private readonly IMapper _mapper;

        public ProductController(IMessageLogger logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public ViewResult List(string category)
        {
            IEnumerable<Product> products = null;
            if (string.IsNullOrEmpty(category))
            {
                products = _unitOfWork.Product.GetProducts();
            }
            else
            {
                switch (category)
                {
                    case "Clothes":
                        products = _unitOfWork.Product.Find(p => p.Category.CategoryName == category);
                        break;
                    case "Furniture":
                        products = _unitOfWork.Product.Find(p => p.Category.CategoryName == category);
                        break;
                    case "Electronics":
                        products = _unitOfWork.Product.Find(p => p.Category.CategoryName == category);
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

            var product = _unitOfWork.Product.Find(p => p.ProductID == id).FirstOrDefault();
                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}