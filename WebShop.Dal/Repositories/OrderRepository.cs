﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public Order CreateOrder(PaymentStatus response) //Method which creates and saves order when payment is authorized.
        {
            var order = new Order();

            order.OrderPlaced = DateTime.Now;
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

            WebShopDbContext.Add(order);

            return order;
        }

        public List<OrderDetail> AddDetailsToOrder(List<ShoppingCartItem> items, int orderId)
        { 
        List<OrderDetail> Details = new List<OrderDetail>();

            foreach (var item in items)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductID = item.Product.ProductID,
                    OrderId = orderId,
                    Price = item.Product.Price
                };
                Details.Add(orderDetail);
                WebShopDbContext.OrderDetails.Add(orderDetail);
            }
            return Details;
        }

        public List<Item> GetItemsFromOrder(int orderId)
        {
            var itemList = new List<Item>();
            var temp = WebShopDbContext.OrderDetails.Where(d => d.OrderId == orderId).Include(d => d.Product).ToList();
            foreach (var item in temp)
            {
                itemList.Add(new Item()
                {
                    Amount = (int)item.Price,
                    Description = item.Product.Description
                });
            }
            return itemList;
        }

        public void Update(Order order)
        {
            WebShopDbContext.Update(order);
        }
    }
}
