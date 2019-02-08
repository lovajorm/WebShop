namespace WebShop.Avarda.Api.Avarda
{
    public class InvoiceResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float CreditLimit { get; set; }
        public string AccountNumber { get; set; }
        public bool RequireDeliveryAuthorization { get; set; }
    }
}
