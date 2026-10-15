using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SubscriptionBilling_;

public class DunningEmailService
{
    private readonly ProrationService _prorationService;
    private readonly InvoiceNumberingService _invoiceNumberingService;

    public DunningEmailService(ProrationService prorationService, InvoiceNumberingService invoiceNumberingService)
    {
        _prorationService = prorationService;
        _invoiceNumberingService = invoiceNumberingService;
    }

    public string DunningEmail(SubscriptionBilling subscription,string customerName, DateOnly asOf)
    {
        // Collections tone & legal boilerplate ≠ proration formula.
        var amount = _prorationService.Prorate(subscription,subscription.PeriodStart);
        var invoice = _invoiceNumberingService.NextInvoiceNumber(subscription); // side-effect while composing mail — nasty on purpose
        var severity = subscription.FailedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };
        return $"Subject: {severity} {invoice}\nHi {customerName},\nBalance {amount:C} as of {asOf:o} ({subscription.FailedPayments} failures).\n";
    }
}
