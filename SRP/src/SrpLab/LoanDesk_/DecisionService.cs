using SrpLab;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.LoanDesk_;

public class DecisionService
{
    private readonly EligibleService _eligibleService;
    private readonly RequiredDocument _requiredDocument;

    public DecisionService(EligibleService eligibleService, RequiredDocument requiredDocument)
    {
        _eligibleService = eligibleService;
        _requiredDocument = requiredDocument;
    }

    public string DecisionLetter(string applicantName,LoanDesk loanDesk)
    {
        // Legal/comms wording ≠ underwriting math.
        if (_eligibleService.IsEligible(loanDesk))
        {
            return $"Dear {applicantName},\nYour request for {loanDesk.RequestedAmount:C} is pre-approved (risk {_eligibleService.RiskScore(loanDesk):0}).\n" +
                   $"Please upload: {string.Join("; ", _requiredDocument.RequiredDocuments(loanDesk))}.\n";
        }

        return $"Dear {applicantName},\nWe are unable to approve {loanDesk.RequestedAmount:C} at this time.\n" +
               $"Reference risk={_eligibleService.RiskScore(loanDesk):0}. You may reapply after improving documentation.\n";
    }
}
