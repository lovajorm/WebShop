using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Dal;
using Microsoft.EntityFrameworkCore;
using WebShop.Dal.Interfaces;
using WebShop.Dal.UoW;
using WebShop.Log;
using WebShop.Web.Models;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        private IUnitOfWork _context;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart, IUnitOfWork context)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
            _context = context;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,//(ShoppingCart)_context.ShoppingCart,//(ShoppingCartRepository)_context.ShoppingCart,//_shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(scvm);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            //var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            var selectedProduct = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                //_context.ShoppingCart.AddToCart(selectedProduct, 1);
                _shoppingCart.AddToCart(selectedProduct, 1);
            }

            return RedirectToAction("List", "Product", new { area = "" });
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            //var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            var selectedProduct = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                //_context.ShoppingCart.RemoveFromCart(selectedProduct);
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}