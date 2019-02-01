using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Web.Models.Avarda;

namespace WebShop.Web.Controllers
{ 

    

    public class AvardaController : Controller
    {
    // GET: Avarda
    public ActionResult Index()
    {
        var connectionDetails = new ConnectionDetails
        {
            User = "TestSweden1",
            Password = "test1",
            Url = "https://stage.avarda.org/Checkout2/CheckOut2Api/InitializePayment"
        };
        
        

        var requestHandler = new RequestHandler();
        var paymentResponse = requestHandler.GetPaymentResponse(connectionDetails);
        return View("Index", paymentResponse);
    }

    private InitializePaymentRequest CreateRequest()
    {
        var i = new InitializePaymentRequest();
        i.Description = "new description";
        i.Mail = "test@test.test";
        i.Price = 1;
        var item = new Items();
        item.Description = "NEW iTEM";
        item.Amount = 1;
        var items = new List<Items>();
        items.Add(item);
        i.Items = items;

            return i;
    }
}
}