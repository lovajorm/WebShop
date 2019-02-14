using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class ExtraPurchaseViewModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string PurchaseId { get; set; }
    }
}
