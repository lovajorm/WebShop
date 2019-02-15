using System.Collections.Generic;

namespace WebShop.Avarda.Api.Avarda
{
    public class PurchaseOrderRequest
    {
        public string ExternalId { get; set; }
        public List<Item> Items { get; set; }
        public int OrderReference { get; set; }
        public string TranId { get; set; }
        public string TrackingCode { get; set; }
        public string PosId { get; set; }
    }
}
