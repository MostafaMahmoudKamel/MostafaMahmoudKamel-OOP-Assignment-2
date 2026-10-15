using SrpLab;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.LoanDesk_;

public class EligibleService
{
    //لو معندوش ضامن هيعطيله 100 بس 
    public decimal RiskScore(LoanDesk loan)
    {
        // Risk model will change with risk committee — not with letter templates.
        decimal score = 100m;
        score -= Math.Max(0, 700 - loan.CreditScore) * 0.15m;
        if (loan.EmploymentMonths < 6) score -= 20m;
        if (loan.RequestedAmount > 50_000m && !loan.HasCollateral) score -= 25m;
        if (loan.RequestedAmount > 150_000m) score -= 10m;
        return Math.Clamp(score, 0m, 100m);
    }

    //هل هيقدر ياخد القرض 
    public bool IsEligible(LoanDesk loan) => RiskScore(loan) >= 55m && loan.CreditScore >= 580;
}
