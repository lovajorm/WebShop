using System;
using System.Collections.Generic;
using WebShop.Avarda.Api.Avarda;
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
        public List<OrderDetail> CreateOrder(Order order, PaymentStatus response)                        //Method which creates and saves order when payment is authorized.
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            order.OrderPlaced = DateTime.Now;

            //var total = _shoppingCart.GetShoppingCartTotal();
            //order.OrderTotal = total;

            order.Ssn = response.AccountNumber;
            order.OrderTotal = response.Price;
            order.Email = response.Mail;
            order.FirstName = response.InvoicingFirstName;
            order.LastName = response.InvoicingLastName;
            order.Address1 = response.InvoicingAddressLine1;
            order.Address2 = response.InvoicingAddressLine2;
            order.ZipCode = response.InvoicingZip;
            order.City = response.InvoicingCity;
            order.Country = response.CountryCode;
            order.PurchaseId = response.PurchaseId;

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
