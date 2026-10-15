using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Kitchen_Ticket;

public class TimeService
{
    private readonly DetectErgensService _detectErgensService;

    public TimeService(DetectErgensService detectErgensService)
    {
        _detectErgensService = detectErgensService;
    }

    //حساب وقت تجهيز الطعام 
    public int EstimatedReadyMinutes(KitchenTicket kitchenTicket,int openStations)
    {
        // Kitchen ops model ≠ printing.
        if (openStations <= 0) openStations = 1;
        
        var sequential = kitchenTicket.Items.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);
        if (_detectErgensService.DetectAllergens(kitchenTicket).Count > 0) parallel += 3; // allergy protocol delay mixed in
        var longest = kitchenTicket.Items.Count == 0 ? 0 : kitchenTicket.Items.Max(i => i.PrepMinutes);
        return Math.Max(parallel, longest);
    }
}
