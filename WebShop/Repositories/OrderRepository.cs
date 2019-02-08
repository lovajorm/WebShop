using System;
using System.Collections.Generic;
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

        
        //Happens on "Complete checkout"
        public List<OrderDetail> CreateOrder(Order order)                        //Method which creates and saves order when payment is authorized.
        {


            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            order.OrderPlaced = DateTime.Now;

            var total = _shoppingCart.GetShopppingCartTotal();
            order.OrderTotal = total;

            _context.Orders.Add(order);

            List<OrderDetail> Details = new List<OrderDetail>(); 
            
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
                Details.Add(orderDetail);
            }

            _context.SaveChanges();

            return Details;
        }
    }
}
