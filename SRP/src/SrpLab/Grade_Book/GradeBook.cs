namespace SRP.src.SrpLab.Grade_Book;

/// <summary>
/// Grade book: aggregation, letter-band policy, and transcript export.
/// </summary>
public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    // Read-only exposure for services
    public IReadOnlyDictionary<string, IReadOnlyList<decimal>> Scores
        => _scores.ToDictionary(k => k.Key, v => (IReadOnlyList<decimal>)v.Value.AsReadOnly());
    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }

 

   
   
}
