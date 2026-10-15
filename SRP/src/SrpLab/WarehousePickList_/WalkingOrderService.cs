using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WarehousePickList_;

public  class WalkingOrderService
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder(WarehousePickList warehousePick)
    {
        // Path heuristic will change with warehouse layout tech.
        return warehousePick.Lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (l.Aisle, l.Bin, l.Sku, Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}
