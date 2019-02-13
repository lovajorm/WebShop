using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Dal.Repositories;
using WebShop.Dal.UoW;

namespace WebShop.Web.Models
{
    public class ShoppingCart
    {
        private readonly IUnitOfWork _UnitOfWork;

        //private readonly WebShopDbContext _context;

        public WebShopDbContext _context;

        public ShoppingCart(IUnitOfWork _unitOfWork)//, WebShopDbContext context)
        {
            _UnitOfWork = _unitOfWork;
            //_context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<IUnitOfWork>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) {ShoppingCartId = cartId};
        }

        public void AddToCart(Product product, int amount) //Method which allows user to add items to cart when in the shop.
        {
            var shoppingCartItem = _UnitOfWork.ShoppingCart.SingleOrDefault(s =>
                s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                _UnitOfWork.ShoppingCart.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _UnitOfWork.Complete();
        }

        public int RemoveFromCart(Product product) //Method which allows user to remove items when in shopping cart.
        {
            var shoppingCartItem = _UnitOfWork.ShoppingCart.SingleOrDefault(
                s => s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _UnitOfWork.ShoppingCart.Remove(shoppingCartItem);
                }
            }

            _UnitOfWork.Complete();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {

            return ShoppingCartItems ??
                   (ShoppingCartItems = _UnitOfWork.ShoppingCart.Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Product)
                       .ToList());
        }

        public void ClearCart() //Clears the cart after "Complete Order".
        {
            var cartItems = _UnitOfWork.ShoppingCart
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _UnitOfWork.ShoppingCart.RemoveRange(cartItems);

            _UnitOfWork.Complete();
        }

        public float GetShoppingCartTotal() //Counts the total price of the cart.
        {
            var total = _UnitOfWork.ShoppingCart.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();

            return total;
        }

        public List<OrderDetail> GetOrderDetailList(int id)
        {
            var details = new List<OrderDetail>();
            foreach (var item in GetShoppingCartItems())
            {
                details.Add(new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductID = item.Product.ProductID,
                    OrderId = id,
                    Price = item.Product.Price
                });
            }
            return details;
        }
    }
}
