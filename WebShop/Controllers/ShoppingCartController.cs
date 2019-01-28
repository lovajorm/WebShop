using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace WebShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly Product _product;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(Product product, ShoppingCart shoppingCart)
        {
            _product = product;
            _shoppingCart = shoppingCart;
        }

        public Product product { get; private set; }
        public ShoppingCart shoppingCart { get; private set; }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShopppingCartTotal()
            };
            return View(scvm);
        }

        //public RedirectToActionResult AddToShoppingCart(int productID)
        //{
        //    var selectedProduct = _product.Products.FirstOrDefault(p => p.ProductID == productID);

        //    if (selectedProduct != null)
        //    {
        //        _shoppingCart.AddToCart(selectedProduct, 1);
        //    }

        //    return RedirectToAction("Index");
        //}

        //public RedirectToActionResult RemoveFromShoppingCart(int productID)
        //{
        //    var selectedProduct = _product.Products.FirstOrDefault(p => p.ProductID == productID);

        //    if (selectedProduct != null)
        //    {
        //        _shoppingCart.RemoveFromCart(selectedProduct);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}