using System;
using Microsoft.AspNetCore.Mvc;
using WebShop.Avarda.Api;
using WebShop.Avarda.Api.Avarda;
using WebShop.Bo;
using WebShop.Dal;
using WebShop.Dal.UoW;
using WebShop.Models;
using WebShop.Web.Models;

namespace WebShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private IUnitOfWork _unitOfWork;
        private ConnectionHandler _getCustomer;

        public OrderController(ShoppingCart shoppingCart, IUnitOfWork unitOfWork)
        {
            _shoppingCart = shoppingCart;
            _unitOfWork = unitOfWork;
            _getCustomer = new ConnectionHandler();
        }

        [HttpGet]
        public JsonResult GetInformation(string ssn)              //Method which gets customer information by using Ssn, see checkout.cshtml.
        {
            try
            {
                var info = _getCustomer.GetCustomerInfo(ssn);
                return Json(info);
            }
            catch (Exception ex)
            //If exception is caught, will show error message
            {
                return Json(new ErrorViewModel { ErrorMessage = $"Failed to get customer. Error: {ex.Message}" });
            }
        }

        [HttpPost] //send authorization to web api
        public IActionResult AuthorizeInvoice(InvoiceRequest request, Order order)
        {
            //var total = _shoppingCart.GetShoppingCartTotal();
            //order.OrderTotal = total;

            //try
            //{
            //    var response = _getCustomer.AuthorizeInvoice(request, order);

            //    if (total < response.Result.CreditLimit)//In tests the creditLimit is not fixed, which means that the if-statement is unnecessary but we will keep it for fun.
            //    {
            //        //Save order and take customer to final page
            //        order.OrderDetails = _context.Order.CreateOrder(order);
            //        _shoppingCart.ClearCart();//Clears cart after "Complete order"
            //        return View("CheckoutComplete", order);
            //    }
            //    else
            //    {
            //        return View("Error", new ErrorViewModel { ErrorMessage = $"Your credit score is too low" });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return View("Error", new ErrorViewModel { ErrorMessage = $"Couldn't get credit score. Error Message: {ex.Message}" });
            //}
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _unitOfWork.ShoppingCart.GetShoppingCartTotal();
            order.OrderDetails = _unitOfWork.ShoppingCart.GetOrderDetailList(order.OrderId);
            try
            {
                var response = _getCustomer.AuthorizeInvoice(request, order);

                if (order.OrderTotal < response.Result.CreditLimit)
                {
                    _unitOfWork.Order.Add(order);
                    _unitOfWork.ShoppingCart.ClearCart();
                    _unitOfWork.Complete();
                    return View("CheckoutComplete", order);
                }
                return View("Error", new ErrorViewModel { ErrorMessage = $"Your credit score is too low" });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { ErrorMessage = $"Couldn't get credit score. Error Message: {ex.Message}" });
            }
        }

        public IActionResult Checkout()//"Check out" from shopping cart to information form.
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            //_context.ShoppingCart.ShoppingCartItems = _context.ShoppingCart.GetShoppingCartItems();
            
            if (_shoppingCart.ShoppingCartItems.Count == 0)//Check to see if the shopping cart contains any items.
            {
                return View("Error", new ErrorViewModel { ErrorMessage = "Your cart is empty, add some products first" }); //Error message shown if you try to check out order without any items in the cart.
            }
            return View();
        }
    }
}