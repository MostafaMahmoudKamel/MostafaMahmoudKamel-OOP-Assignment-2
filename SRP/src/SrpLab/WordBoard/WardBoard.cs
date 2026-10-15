namespace SRP.src.SrpLab.WordBoard;

/// <summary>
/// Hospital ward board: tracks beds,
/// computes acuity scores,
/// drafts nurse handoff notes,
/// and decides which pager code to fire. Looks like "one ward concern" — it is not.
/// </summary>
public sealed class WardBoard
{
    private readonly Dictionary<int, string> _bedPatient = new();//رقم المريض
    private readonly Dictionary<int, int> _vitalsScore = new();//حاله المريض
    private readonly List<string> _pagerLog = new();//log

    private readonly AcuityScoring _acuityScoring;
    private readonly WordLogger _wordLogger;

    public WardBoard(AcuityScoring acuityScoring, WordLogger wordLogger)
    {
        _acuityScoring = acuityScoring;
        _wordLogger = wordLogger;
    }



    public void AssignBed(int bed, string patientId, int heartRate, int spo2)
    {
        if (bed <= 0) throw new ArgumentOutOfRangeException(nameof(bed));
        if (string.IsNullOrWhiteSpace(patientId)) throw new ArgumentException("patient required");
        
        _bedPatient[bed] = patientId.Trim().ToUpperInvariant();
        _vitalsScore[bed] = _acuityScoring.ScoreAcuity(heartRate, spo2);


        _wordLogger.CheckAndLogAlert(bed, _vitalsScore[bed]);

    }


    // دوال مساعدة عشان باقي الكلاسات (زي التقارير واللوجر) تعرف تقرأ البيانات
    public IReadOnlyDictionary<int, string> GetBedPatients() => _bedPatient;
    public IReadOnlyDictionary<int, int> GetVitalsScores() => _vitalsScore;

}
