using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.CheckoutBasket;

public class GiftMessageService
{
    public string GiftMessageCard(string fromName,IEnumerable<(string Sku,decimal Price,int Qty)> lines,decimal grandTotal)
    {
        // Customer-facing copy will change with marketing, not with totals.
        var items = string.Join(", ", lines.Select(l => l.Sku));
        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }

}
