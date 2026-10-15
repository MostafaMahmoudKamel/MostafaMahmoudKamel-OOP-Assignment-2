using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WarehousePickList_;

public class PickerScriptService
{
    private readonly WalkingOrderService _walkingOrderService;
    private readonly AllocateService _allocateService;

    public PickerScriptService(WalkingOrderService walkingOrderService, AllocateService allocateService)
    {
        _walkingOrderService = walkingOrderService;
        _allocateService = allocateService;
    }

    public string PickerScript(WarehousePickList warehousePick)
    {
        // UX wording for handheld devices — separate owners.
        var steps = _walkingOrderService.WalkingOrder(warehousePick)
            .Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        var shortfalls = _allocateService.Allocate(warehousePick).Where(a =>
        {
            var need = warehousePick.Lines.First(l => l.Sku == a.Sku).QtyNeeded;
            return a.Allocated < need;
        });
        var warn = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";
        return string.Join('\n', steps) + "\n" + warn;
    }
}
