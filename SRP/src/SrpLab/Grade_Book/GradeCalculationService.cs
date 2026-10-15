using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Grade_Book;

public class GradeCalculationService
{
    public decimal Average(GradeBook grade,string studentId)
    {
        if (! grade.Scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }

    public string Letter(GradeBook grade,string studentId)
    {
        // Academic policy bands change with faculty senate — not with CSV layout.
        var avg = Average(grade,studentId);
        if (avg >= 90) return "A";
        if (avg >= 80) return "B";
        if (avg >= 70) return "C";
        if (avg >= 60) return "D";
        return "F";
    }

    public bool MeetsHonorRoll(GradeBook gradeBook, string studentId)
    {
        return Average(gradeBook, studentId) >= 85 && Letter(gradeBook, studentId) is "A" or "B";
    }
}
