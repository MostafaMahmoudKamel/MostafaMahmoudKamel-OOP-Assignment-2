using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.CheckoutBasket;

public class DiscountService
{
    public decimal DiscountAmount(string? couponRaw,decimal subTotal)
    {
        // Parsing marketing strings is a different reason to change than pricing math.
        if (string.IsNullOrWhiteSpace(couponRaw)) return 0m;
        var t = couponRaw.Trim().ToUpperInvariant();
        if (t.StartsWith("SAVE") && int.TryParse(t[4..], out var pct) && pct is > 0 and <= 50)
            return Math.Round(subTotal* pct / 100m, 2);
        if (t.Contains("FREESHIP")) return 0m; // ship is elsewhere — still parsed here
        if (t == "WELCOME10") return Math.Min(10m, subTotal);
        return 0m;
    }
}
