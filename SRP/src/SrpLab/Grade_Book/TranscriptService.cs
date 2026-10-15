using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Grade_Book;

public class TranscriptService
{
    private readonly GradeCalculationService _calcService;

    public TranscriptService(GradeCalculationService calcService)
    {
        _calcService = calcService;
    }
    public string TranscriptPlain(GradeBook gradeBook, string studentId, string fullName)
    {
        var avg = _calcService.Average(gradeBook, studentId);
        var letter = _calcService.Letter(gradeBook, studentId);
        var honor = _calcService.MeetsHonorRoll(gradeBook, studentId);

        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {avg}\nLetter: {letter}\nHonor: {honor}\n";
    }

}
