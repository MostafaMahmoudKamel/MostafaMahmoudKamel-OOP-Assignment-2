using SRP.src.SrpLab.LoanDesk_;
using SrpLab;
using System;
using System.Collections.Generic;
using System.Text;


internal class ReportService
{
    private readonly EligibleService _eligibleService;

    public ReportService(EligibleService eligibleService)
    {
        _eligibleService = eligibleService;
    }

    public string UnderwriterCsvRow(string applicationId, LoanDesk loanDesk)
    {
        // Analytics export schema is yet another reason to change.
        return $"{applicationId},{loanDesk.CreditScore},{loanDesk.EmploymentMonths},{(loanDesk.HasCollateral ? 1 : 0)},{_eligibleService.RiskScore(loanDesk):0.00},{(_eligibleService.IsEligible(loanDesk) ? "Y" : "N")}";
    }
}
