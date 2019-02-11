using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Dal.Repositories;
using WebShop.Web.Interfaces;

namespace WebShop.Web.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCartItem>, IShoppingCartRepository
    {
            //private readonly WebShopDbContext _context;

            public WebShopDbContext WebShopDbContext => Context as WebShopDbContext;

            public ShoppingCartRepository(WebShopDbContext context) : base(context)
            {
                //_context = webShopDbContext;
            }

            public string ShoppingCartId { get; set; }
            public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        
            public static ShoppingCartRepository GetCart(IServiceProvider services)                                       
            {
                ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

                var context = services.GetService<WebShopDbContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId",cartId);

                return new ShoppingCartRepository(context) {ShoppingCartId = cartId};
            }

            public void AddToCart(Product product, int amount)                                                //Method which allows user to add items to cart when in the shop.
            {
                var shoppingCartItem = ShoppingCartItems.SingleOrDefault(s =>
                    s.Product.ProductID == product.ProductID && s.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = ShoppingCartId,
                        Product = product,
                        Amount = 1
                    };
                    ShoppingCartItems.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }
                WebShopDbContext.SaveChanges();
            }

            public int RemoveFromCart(Product product)                                          //Method which allows user to remove items when in shopping cart.
            {
                var shoppingCartItem = ShoppingCartItems.SingleOrDefault(
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
                        ShoppingCartItems.Remove(shoppingCartItem);
                    }
                }
                WebShopDbContext.SaveChanges();

                return localAmount;
            }

            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                return ShoppingCartItems ??
                       (ShoppingCartItems = WebShopDbContext.ShoppingCart.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList());
            }

            public void ClearCart()                                                                 //Clears the cart after "Complete Order".
            {
                var cartItems = ShoppingCartItems
                    .Where(cart => cart.ShoppingCartId == ShoppingCartId);

                ShoppingCartItems.RemoveRange(0, cartItems.Count());

                //WebShopDbContext.SaveChanges();
            }

            public float GetShoppingCartTotal()                                                    //Counts the total price of the cart.
            {
                var total = ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Product.Price * c.Amount).Sum();

                return total;
            }

        public List<OrderDetail> GetOrderDetailList(int id)
        {
            //var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            //order.OrderPlaced = DateTime.Now;

            //var total = _shoppingCart.GetShoppingCartTotal();
            //order.OrderTotal = total;

            //_context.Orders.Add(order);

            //List<OrderDetail> Details = new List<OrderDetail>(); 

            //foreach (var item in shoppingCartItems)
            //{
            //    var orderDetail = new OrderDetail()
            //    {
            //        Amount = item.Amount,
            //        ProductID = item.Product.ProductID,
            //        OrderId = order.OrderId,
            //        Price = item.Product.Price
            //    };
            //    _context.OrderDetails.Add(orderDetail);
            //    Details.Add(orderDetail);
            //}
            //_context.SaveChanges();

            //return Details;

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
                //WebShopDbContext.OrderDetails.Add();  //is it necessary to save orderdetails?
            }

            return details;
        }
    }
}
