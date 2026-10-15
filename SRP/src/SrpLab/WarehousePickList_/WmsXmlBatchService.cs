using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WarehousePickList_;

public class WmsXmlBatchService
{
    private readonly AllocateService _allocateService;

    public WmsXmlBatchService(AllocateService allocateService)
    {
        _allocateService = allocateService;
    }

    public string WmsXmlBatch(WarehousePickList warehousePick,string batchId)
    {
        // Integration contract with WMS — third reason to change.
        var parts = _allocateService.Allocate(warehousePick).Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
