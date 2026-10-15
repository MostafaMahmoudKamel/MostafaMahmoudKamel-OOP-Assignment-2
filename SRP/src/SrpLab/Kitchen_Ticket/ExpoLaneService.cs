using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Kitchen_Ticket;



public class ExpoLaneService
{
    private readonly DetectErgensService _detectErgensService;
    private readonly TimeService _timeService;

    public ExpoLaneService(DetectErgensService detectErgensService, TimeService timeService)
    {
        _detectErgensService = detectErgensService;
        _timeService = timeService;
    }

    
    public string ExpoLaneHint(KitchenTicket kitchenTicket)
    {
        return _detectErgensService.DetectAllergens(kitchenTicket).Count > 0 ? "LANE-ALLERGY" : _timeService.EstimatedReadyMinutes(kitchenTicket,2) > 20 ? "LANE-SLOW" : "LANE-FAST";
    }
}
