using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WordBoard;

public class WordLogger
{
    public readonly List<string> _pagerLog = new();

    public IReadOnlyList<string> DrainPagerLog()
    {
        var copy = _pagerLog.ToList();
        _pagerLog.Clear();
        return copy;
    }

    public void CheckAndLogAlert(int bed,int acuity)
    {
        if (acuity >= 8)
        {
            _pagerLog.Add($"CODE-YELLOW bed={bed} at {DateTime.UtcNow:HH:mm}");
        }
    }
}
