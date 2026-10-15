using SRP.src.SrpLab.WarehousePickList_;
using System;
using System.Collections.Generic;
using System.Text;


public class AllocateService
{
    //خدمة تخصيص المخزون:  
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(WarehousePickList warehouse)
    {
        // Allocation/backorder policy ≠ walking path ≠ human instructions.
        var result = new List<(string, int)>();
        foreach (var line in warehouse.Lines)
        {
            var alloc = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, alloc));
        }
        return result;
    }
}
