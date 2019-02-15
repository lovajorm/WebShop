using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Avarda.Api.Avarda
{
    public class PaymentStatus
    {
        public string AccountNumber { get; set; }
        public string PurchaseId { get; set; }
        public int State { get; set; }
        public float Price { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string InvoicingFirstName { get; set; }
        public string InvoicingLastName { get; set; }
        public string InvoicingAddressLine1 { get; set; }
        public string InvoicingAddressLine2 { get; set; }
        public string InvoicingZip { get; set; }
        public string InvoicingCity { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public string CustomerToken { get; set; }
        public string OrderReference { get; set; }
    }
}
