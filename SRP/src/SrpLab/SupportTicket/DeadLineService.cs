using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SupportTicket;

public class DeadLineService
{
    public DateTimeOffset SlaDeadline(string priority, DateTimeOffset openedAt)
    {
        // Operational SLA policy — different stakeholders than reply wording.
        var hours = priority switch
        {
            "P1" => 4,
            "P2" => 24,
            _ => 72
        };
        return openedAt.AddHours(hours);
    }

    public bool IsBreached(string priority,DateTimeOffset openedAt,DateTimeOffset now) 
        => now > SlaDeadline(priority,openedAt);

}
