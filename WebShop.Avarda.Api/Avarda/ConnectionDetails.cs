namespace WebShop.Avarda.Api.Avarda
{
    class ConnectionDetails
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public override string ToString() => $"{UserName}:{Password}";
    }
}
