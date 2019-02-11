using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebShop.Log;
using WebShop.Web.ViewModels;
using WebShop.Web.Repositories;
using WebShop.Web.UoW;

namespace WebShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        //private readonly ShoppingCartRepository _shoppingCart;
        private UnitOfWork _context;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCartRepository shoppingCart, WebShopDbContext context)
        {
            _productRepository = productRepository;
            //_shoppingCart = shoppingCart;
            _context = new UnitOfWork(context);
        }

        public ViewResult Index()
        {
            var items = _context.ShoppingCart.GetShoppingCartItems();
            _context.ShoppingCart.ShoppingCartItems = items;
            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = (ShoppingCartRepository)_context.ShoppingCart,//(ShoppingCartRepository)_context.ShoppingCart,//_shoppingCart,
                ShoppingCartTotal = _context.ShoppingCart.GetShoppingCartTotal()
            };
            return View(scvm);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            //var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            var selectedProduct = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _context.ShoppingCart.AddToCart(selectedProduct, 1);
            }

            return RedirectToAction("List", "Product", new { area = "" });
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            //var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            var selectedProduct = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _context.ShoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}