using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.SubscriptionBilling_;

public class ProrationService
{
    //حساب قيمه الاشتراك 
    public decimal Prorate(SubscriptionBilling subscription,DateOnly activeFrom)
    {
        // Finance calendar rules change independently of email copy.
        if (activeFrom <= subscription.PeriodStart) return subscription.MonthlyPrice;
        if (activeFrom >= subscription.PeriodEnd) return 0m;
        var totalDays = subscription.PeriodEnd.DayNumber - subscription.PeriodStart.DayNumber;
        if (totalDays <= 0) return subscription.MonthlyPrice;
        var used = subscription.PeriodEnd.DayNumber - activeFrom.DayNumber;
        return Math.Round(subscription.MonthlyPrice * used / totalDays, 2);
    }
}
