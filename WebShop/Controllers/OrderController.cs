using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Common;
using WebShop.Dal.UoW;
using WebShop.Models;
using WebShop.Web.Models;
using WebShop.Web.ViewModels;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailHandler _emailHandler;
        private readonly ConnectionHandler _connectionHandler;

        public OrderController(IUnitOfWork unitOfWork, ShoppingCart shoppingCart, IEmailHandler emailHandler)
        {
            _shoppingCart = shoppingCart;
            _emailHandler = emailHandler;
            _connectionHandler = new ConnectionHandler();
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult InitializePayment(string purchaseId, string callback, string paymentStatus)
        {
            var purchaseIdentification = new PaymentResponse
            {
                PurchaseId = purchaseId
            };

            if (!string.IsNullOrWhiteSpace(callback) && (callback.Equals("1") || callback.Equals("2")))
            {
                //Load iframe with original purchaseid submitted in the Querystring
                //no initializePurchase should be called
                //Callback == 1 then the call back is due to card payment
                //Callback == 2 then the call back is due to session cookie setting for safari
                return View("Avarda", purchaseIdentification);
            }

            if (!string.IsNullOrWhiteSpace(paymentStatus) && !string.IsNullOrWhiteSpace(purchaseId))
            {
                if (paymentStatus.Equals("Success"))
                {
                    //successfull direct bank payment detected - redirect to done page.
                    return RedirectToAction("Done", new {purchaseid = purchaseId});
                }

                //unsuccessfull direct bank payment -
                //Load iframe with original purchaseid submitted in querystring
                //no intializePurchase should be called
                return View("Avarda", purchaseIdentification);
            }

            //no callback detected - treat the request as new purchase.
            //call initializePurchase and get purchaseid
            try
            {
                var request = new PaymentRequest
                {
                    Price = _shoppingCart.GetShoppingCartTotal(),
                    Items = ConvertShoppingCartItemToItem()
                };
                //request.OrderReference = "4444";

                var response = _connectionHandler.InitializePayment(request);
                
                return View("Avarda", response);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = $"Something went wrong: {ex.Message}" });
            }
        }

        private List<Item> ConvertShoppingCartItemToItem()
        {
            var itemList = new List<Item>();

            foreach (var item in _shoppingCart.GetShoppingCartItems())
            {
                itemList.Add(new Item
                {
                    Amount = (int)item.Product.Price,
                    Description = item.Product.Title
                });
            }
            return itemList;
        }


        public IActionResult Done(string purchaseId, Order order)
        {
            var response = _connectionHandler.GetPaymentStatus(purchaseId);
            var prodId = 7;
            var product = _unitOfWork.Product.Get(prodId);

            var purchaseViewModel = new ExtraPurchaseViewModel
            {
                Product = product,
                ProductId = prodId,
                PurchaseId = purchaseId
            };

            if (response.State == 2)
            {
                var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
                _unitOfWork.Order.CreateOrder(order, response, shoppingCartItems);
                
                switch (response.PaymentMethod)
                {
                    case PaymentMethodEnum.Invoice:
                    case PaymentMethodEnum.Swish:
                        ViewData["description"] = purchaseId;

                        return View(purchaseViewModel);
                        
                    default:
                        _shoppingCart.ClearCart();
                        return View("CheckoutComplete", order);
                }
            }
            return View("Error", new ErrorViewModel { ErrorMessage = $"Payment failed." });
        }

        public IActionResult PurchaseOrder(ExtraPurchaseViewModel purchaseViewModel)
        {
            var order = _unitOfWork.Order.Find(o => o.PurchaseId == purchaseViewModel.PurchaseId).FirstOrDefault();
            
            var request = new PurchaseOrderRequest()
            {
                ExternalId = purchaseViewModel.PurchaseId,
                Items = _unitOfWork.Order.GetItemsFromOrder(order.OrderId)
            };

            _shoppingCart.ClearCart();
            
            _connectionHandler.PurchaseOrder(request);

            return View("CheckoutComplete", order);
        }

        public IActionResult AddUpSaleToOrder(ExtraPurchaseViewModel purchaseViewModel)
        {
            var order = _unitOfWork.Order.Find(o => o.PurchaseId == purchaseViewModel.PurchaseId).FirstOrDefault();
            //get product
            var product = _unitOfWork.Product.Get(purchaseViewModel.ProductId);

            //convert product to orderdetail
            var orderDetail = _unitOfWork.OrderDetail.ConvertProductToOrderDetail(product, order.OrderId);

            //add orderdetail in db
            _unitOfWork.OrderDetail.Add(orderDetail);
            _unitOfWork.Complete();
            
            //get orderdetails, sum price, update db
            order.OrderTotal = _unitOfWork.OrderDetail.Find(d => d.OrderId == order.OrderId).Sum(i => i.Price * i.Amount);
            _unitOfWork.Order.Update(order);
            _unitOfWork.Complete();

            return RedirectToAction("PurchaseOrder", purchaseViewModel);
        }
    }
}