using System;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Web.Interfaces;
using WebShop.Web.Models;

namespace WebShop.Web.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebShopDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(WebShopDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)                        //Method which creates an order on "Check out" when in shopping cart.
        {
            order.OrderPlaced = DateTime.Now;                       //TODO remove or change method when auth is done

            var total = order.OrderTotal;
            total = _shoppingCart.GetShopppingCartTotal();

            _context.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductID = item.Product.ProductID,
                    OrderId = order.OrderId,
                    Price = item.Product.Price
                };
                _context.OrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();
        }
    }
}
