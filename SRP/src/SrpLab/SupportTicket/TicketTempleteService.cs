using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SupportTicket;

public class TicketTempleteService
{
    private readonly DeadLineService _deadLineService;

    public TicketTempleteService(DeadLineService deadLineService)
    {
        _deadLineService = deadLineService;
    }
    public string DraftPublicReply(string agentName,SupportTicket ticket)
    {
        // Tone/templates owned by CX — not by SLA engineering.
        var apology = ticket.Priority == "P1" ? "We are treating this as a critical incident." : "Thanks for reaching out.";
        return $"Hi,\n{apology}\nTicket {ticket.Id} is with {agentName}. Next update before {_deadLineService.SlaDeadline(ticket.Priority,ticket.OpenedAt):u}.\n";
    }

    public string InternalEscalationBlurb(SupportTicket ticket)
    {
        return $"ESCALATE {ticket.Id} priority={ticket.Priority} breachAt={_deadLineService.SlaDeadline(ticket.Priority, ticket.OpenedAt):u} keywords-scanned=yes";
    }
}
