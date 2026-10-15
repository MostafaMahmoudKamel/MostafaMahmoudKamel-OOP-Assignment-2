using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WordBoard;

public class AcuityScoring
{

    //HeartRate : ضربات القلب , spo: Oxygen
    //حساب مدي خطوره حاله المريض
    public int ScoreAcuity(int heartRate, int spo2)
    {
        // Clinical scoring mixed with arbitrary pager thresholds (policy will churn separately).
        var score = 0;
        if (heartRate > 120 || heartRate < 45) score += 4;
        else if (heartRate > 100) score += 2;
        if (spo2 < 90) score += 5;
        else if (spo2 < 94) score += 2;
        return Math.Min(score, 10);
    }
}
