using Microsoft.AspNetCore.Mvc;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;
using WebShop.Web.Repositories;
using WebShop.Web.UoW;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        private WebShopDbContext _context;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
            //_context = new UnitOfWork(context);
        }
        
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }
    }
}
