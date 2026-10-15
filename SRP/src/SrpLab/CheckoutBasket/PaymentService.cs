using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.CheckoutBasket;

public class PaymentService
{
    public string AuthorizePaymentStub(string cardLast4,decimal grandTotal,int linesCount)
    {
        // Pretends to talk to a gateway — auth scheme changes independently of cart rules.
        var payload = $"{grandTotal:0.00}|{cardLast4}|{linesCount}";
        var hash = payload.GetHashCode();
        return $"AUTH-{Math.Abs(hash):X8}";
    }
}
