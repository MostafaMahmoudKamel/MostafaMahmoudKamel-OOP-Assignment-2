using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SupportTicket;

public class PriorityService
{
    public string RecalculatePriorityFromText(string subject,string body)
    {
        // Keyword heuristics will churn with support playbooks; SLA math will not.
        var blob = (subject + " " + body).ToLowerInvariant();
        if (blob.Contains("down") || blob.Contains("outage") || blob.Contains("cannot login"))
            return "P1";
        else if (blob.Contains("urgent") || blob.Contains("asap") || blob.Contains("blocked"))
            return  "P2";
        else
            return "P3";
    }
}
