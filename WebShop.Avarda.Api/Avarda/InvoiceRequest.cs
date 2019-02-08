namespace WebShop.Avarda.Api.Avarda
{
    public class InvoiceRequest
    {
        public string Ssn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public float Amount { get; set; }
        public string AccountClass = "7";
        public string CellPhone = "000000000";
        public string Email = "sample@email.com";

    }
}
