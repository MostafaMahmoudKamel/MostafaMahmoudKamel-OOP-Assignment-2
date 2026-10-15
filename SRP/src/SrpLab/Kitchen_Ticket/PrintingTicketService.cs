using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Kitchen_Ticket;

public class PrintingTicketService
{
    private readonly DetectErgensService _detectErgensService;
    private readonly TimeService _timeService;

    public PrintingTicketService(DetectErgensService detectErgensService, TimeService timeService)
    {
        _detectErgensService = detectErgensService;
        _timeService = timeService;
    }

    //طباعه الاوردر
    public string RenderThermalTicket(KitchenTicket kitchenTicket,int orderNumber)
    {
        // Hardware/formatting concerns — width, separators — change with printer vendor.
        var width = 32;
        var line = new string('=', width);
        var body = string.Join('\n', kitchenTicket.Items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));
        var allergens = _detectErgensService.DetectAllergens(kitchenTicket);
        var allergyLine = allergens.Count == 0 ? "ALLERGENS: none" : "ALLERGENS: " + string.Join(",", allergens);
        return $"{line}\nORDER #{orderNumber}\nETA {_timeService.EstimatedReadyMinutes(kitchenTicket,2)} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
