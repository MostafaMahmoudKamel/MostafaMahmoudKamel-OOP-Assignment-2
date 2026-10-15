using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SubscriptionBilling_;

public class InvoiceNumberingService
{
    private static int _invoiceSeq = 1000;

    //انشاء رقم للفاتوره
    public string NextInvoiceNumber(SubscriptionBilling subscription)
    {
        // Numbering scheme / fiscal prefixes — ops concern, not pricing.
        var n = ++_invoiceSeq;
        return $"INV-{subscription.PeriodStart:yyyyMM}-{n:D5}";
    }
}
