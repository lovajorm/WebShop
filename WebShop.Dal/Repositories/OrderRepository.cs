using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Dal.Interfaces;

namespace WebShop.Dal.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public IWebShopDbContext WebShopDbContext => Context as IWebShopDbContext;
        public OrderRepository(IWebShopDbContext context) : base(context){}
        //Happens on "Complete checkout"
        public List<OrderDetail> CreateOrder(Order order, PaymentStatus response, List<ShoppingCartItem> items)                        //Method which creates and saves order when payment is authorized.
        {
            //var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
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

            WebShopDbContext.Orders.Add(order);

            List<OrderDetail> Details = new List<OrderDetail>();

            foreach (var item in items)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductID = item.Product.ProductID,
                    OrderId = order.OrderId,
                    Price = item.Product.Price
                };
                WebShopDbContext.OrderDetails.Add(orderDetail);
                Details.Add(orderDetail);
            }
            WebShopDbContext.Complete();

            return Details;
        }
    }
}
