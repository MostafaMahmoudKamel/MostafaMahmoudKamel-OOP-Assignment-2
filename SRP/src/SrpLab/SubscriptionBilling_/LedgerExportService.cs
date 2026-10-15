using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SubscriptionBilling_;

public class LedgerExportService
{
    private readonly ProrationService _prorationService;
    private readonly InvoiceNumberingService _invoiceNumberingService;
    public string LedgerJournalLine(SubscriptionBilling subscription,DateOnly activeFrom)
    {
        // Accounting export format is another axis of change.
        return $"{subscription.CustomerId},{_invoiceNumberingService.NextInvoiceNumber(subscription)},{_prorationService.Prorate(subscription,activeFrom):0.00},AR-SUB";
    }
}
