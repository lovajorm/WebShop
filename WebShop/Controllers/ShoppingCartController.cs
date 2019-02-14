using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dal.UoW;
using WebShop.Web.Models;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork, ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
            _unitOfWork = unitOfWork;
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
            var selectedProduct = _unitOfWork.Product.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }

            return RedirectToAction("List", "Product", new { area = "" });
        }

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _unitOfWork.Product.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }
    }
}