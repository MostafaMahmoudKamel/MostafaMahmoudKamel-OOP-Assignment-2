using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Kitchen_Ticket;

public class DetectErgensService
{
    //كشف مسببات الحساسيه
    public IReadOnlyList<string> DetectAllergens(KitchenTicket kitchenTicket)
    {
        // Regulatory allergen dictionary changes separately from ticket layout.
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var (_, ingredients, _) in kitchenTicket.Items)
        {
            foreach (var ing in ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter")) hits.Add("dairy");
                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread")) hits.Add("gluten");
                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew")) hits.Add("nuts");
                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab")) hits.Add("shellfish");
            }
        }
        return hits.OrderBy(x => x).ToList();
    }
}
