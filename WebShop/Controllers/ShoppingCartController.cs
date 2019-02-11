using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models;
using WebShop.Web.Interfaces;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var scvm = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(scvm);
        }

        public RedirectToActionResult AddToShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }

            return RedirectToAction("List", "Product", new { area = "" });
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}