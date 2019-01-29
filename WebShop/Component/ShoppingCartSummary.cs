﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Bo;
using WebShop.Web.Models;

namespace WebShop.Web.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<ShoppingCartItem>() {new ShoppingCartItem(), new ShoppingCartItem() };//_shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShopppingCartTotal()
            };
            return View(ShoppingCartViewModel);
        }
    }
}