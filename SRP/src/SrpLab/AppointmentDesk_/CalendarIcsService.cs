using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.AppointmentDesk_;
public class CalendarIcsService
{
    public string ToIcs(AppointmentDesk desk, DateTimeOffset slot, string patientName, string clinician)
    {
        var uid = Guid.NewGuid();
        var end = slot.AddMinutes(desk.SlotMinutes);
        return "BEGIN:VCALENDAR\nVERSION:2.0\nBEGIN:VEVENT\n" +
               $"UID:{uid}\nDTSTART:{slot:yyyyMMdd'T'HHmmss'Z'}\nDTEND:{end:yyyyMMdd'T'HHmmss'Z'}\n" +
               $"SUMMARY:Visit {patientName} / {clinician}\nEND:VEVENT\nEND:VCALENDAR\n";
    }
}
