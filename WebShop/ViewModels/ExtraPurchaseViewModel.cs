using WebShop.Bo;

namespace WebShop.Web.ViewModels
{
    public class ExtraPurchaseViewModel : Product
    {
        public Product Product { get; set; }
        public string PurchaseId { get; set; }
        public int ProductId { get; set; }
    }
}
