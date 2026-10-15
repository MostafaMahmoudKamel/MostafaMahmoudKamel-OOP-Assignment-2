using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WordBoard;

public class WordReport
{
    //report for all paitent
    public string ExportCensusCsv(IReadOnlyDictionary<int,string> bedPatient, IReadOnlyDictionary<int,int> vitalsScores)
    {
        // Persistence/export shape mixed into the same type as acuity + paging.
        var lines = new List<string> { "bed,patient,acuity" };
        foreach (var bed in bedPatient.Keys.OrderBy(x => x))
            lines.Add($"{bed},{bedPatient[bed]},{vitalsScores[bed]}");
        return string.Join('\n', lines);
    }

  
}
