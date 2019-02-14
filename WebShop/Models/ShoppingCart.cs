using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Bo;
using WebShop.Dal.UoW;

namespace WebShop.Web.Models
{
    public class ShoppingCart
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCart(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            var shoppingCartItem = _unitOfWork.ShoppingCart.SingleOrDefault(s =>
                s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };
                _unitOfWork.ShoppingCart.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _unitOfWork.Complete();
        }

        public int RemoveFromCart(Product product) //Method which allows user to remove items when in shopping cart.
        {
            var shoppingCartItem = _unitOfWork.ShoppingCart.SingleOrDefault(
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
                    _unitOfWork.ShoppingCart.Remove(shoppingCartItem);
                }
            }

            _unitOfWork.Complete();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {

            return ShoppingCartItems ??
                   (ShoppingCartItems = _unitOfWork.ShoppingCart.Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Product)
                       .ToList());
        }

        public void ClearCart() //Clears the cart after "Complete Order".
        {
            var cartItems = _unitOfWork.ShoppingCart
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);
            
            _unitOfWork.ShoppingCart.RemoveRange(cartItems);
            ShoppingCartItems = null;
            _unitOfWork.Complete();
        }

        public float GetShoppingCartTotal() //Counts the total price of the cart.
        {
            var total = _unitOfWork.ShoppingCart.Where(c => c.ShoppingCartId == ShoppingCartId)
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
