using SRP.src.SrpLab.LoanDesk_;
using SrpLab;
using System;
using System.Collections.Generic;
using System.Text;


public class RequiredDocument
{
    private readonly EligibleService _eligibleService;

    public RequiredDocument(EligibleService eligibleService)
    {
        _eligibleService = eligibleService;
    }

    public IReadOnlyList<string> RequiredDocuments(LoanDesk loanDesk)
    {
        // Compliance checklist changes with regulation, independently of risk formula.
        var docs = new List<string> { "National ID", "Proof of income (3 months)" };
        if (loanDesk.RequestedAmount > 40_000m) docs.Add("Bank statements (6 months)");
        if (loanDesk.HasCollateral) docs.Add("Collateral ownership deed");
        if (loanDesk.EmploymentMonths < 12) docs.Add("Employer letter");
        if (!_eligibleService.IsEligible(loanDesk)) docs.Add("Manual underwriter referral form");
        return docs;
    }
}
