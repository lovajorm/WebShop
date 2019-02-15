using System;
using System.Collections.Generic;

namespace WebShop.Bo
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Ssn { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string PurchaseId { get; set; }
    }
}
