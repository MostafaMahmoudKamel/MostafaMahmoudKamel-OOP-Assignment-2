using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Grade_Book;

public class GradeExportService
{
    private readonly GradeCalculationService _calcService;

    public GradeExportService(GradeCalculationService calcService)
    {
        _calcService = calcService;
    }
    public string ExportCsv(GradeBook gradeBook)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in gradeBook.Scores.Keys.OrderBy(x => x))
            rows.Add($"{id},{_calcService.Average(gradeBook,id)},{_calcService.Letter(gradeBook,id)},{(_calcService.MeetsHonorRoll(gradeBook,id) ? 1 : 0)}");
        return string.Join('\n', rows);
    }
}
