using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Avarda.Api.Avarda
{
    public enum PaymentMethodEnum
    {
        Invocie = 0,
        Loan = 1,
        Card = 2,
        DirectPayment = 3,
        PartPayment = 4,
        Swish = 5,
        HighLoanAmount = 6,
        PayPal = 7,
        PayOnDelivery = 8,
        B2BInvoice = 9,
        Unknown = 99
    }
}
