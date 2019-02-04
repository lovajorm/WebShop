﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Bo;
using WebShop.Dal;

namespace WebShop.Web.Models
{
    public class ShoppingCart
    {
            private readonly WebShopDbContext _context;

            private ShoppingCart(WebShopDbContext webShopDbContext)
            {
                _context = webShopDbContext;
            }

            public string ShoppingCartId { get; set; }
            public List<ShoppingCartItem> ShoppingCartItems { get; set; }



            public static ShoppingCart GetCart(IServiceProvider services)                                       //Loads the shopping cart. Pogram can't run without this, why?
            {
                ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

                var context = services.GetService<WebShopDbContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId",cartId);

                return new ShoppingCart(context) {ShoppingCartId = cartId};
            }

            public void AddToCart(Product product, int amount)                                                //Method which allows user to add items to cart when in the shop.
            {
                var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(s =>
                    s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = ShoppingCartId,
                        Product = product,
                        Amount = 1
                    };
                    _context.ShoppingCartItems.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }

                _context.SaveChanges();
            }

            public int RemoveFromCart(Product product)                                          //Method which allows user to remove items when in shopping cart.
            {
                var shopppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

                var localAmount = 0;

                if (shopppingCartItem != null)
                {
                    if (shopppingCartItem.Amount > 1)
                    {
                        shopppingCartItem.Amount--;
                        localAmount = shopppingCartItem.Amount;
                    }
                    else
                    {
                        _context.ShoppingCartItems.Remove(shopppingCartItem);
                    }
                }
                _context.SaveChanges();

                return localAmount;
            }

            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                return ShoppingCartItems ??
                       (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList());
            }

            public void ClearCart()                                                                 //Clears the cart after "Complete Order".
            {
                var cartItems = _context
                    .ShoppingCartItems
                    .Where(cart => cart.ShoppingCartId == ShoppingCartId);

                _context.ShoppingCartItems.RemoveRange(cartItems);

                _context.SaveChanges();
            }

            public float GetShopppingCartTotal()                                                    //Counts the total price of the cart.
            {
                var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Product.Price * c.Amount).Sum();

                return total;
            }
    }
}
