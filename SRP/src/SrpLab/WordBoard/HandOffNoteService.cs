using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.WordBoard;

public  class HandOffNoteService
{
    public string BuildHandoffNote(int bed, IReadOnlyDictionary<int, string> bedPatient, IReadOnlyDictionary<int, int> vitalsScores)
    {
        if (!bedPatient.TryGetValue(bed, out var patient))
            return $"Bed {bed}: empty";

        var acuity = vitalsScores[bed];
        var tone = acuity >= 8 ? "ESCALATE" : acuity >= 4 ? "WATCH" : "STABLE";
        // Presentation / narrative format will change without clinical rules changing.
        return $"[HANDOFF {DateTime.UtcNow:yyyy-MM-dd}] Bed {bed} · {patient} · acuity={acuity} · {tone}";
    }
}
