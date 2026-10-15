using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.AppointmentDesk_;

public class SmsReminderService
{
    public string SmsReminder(DateTimeOffset slot, string clinicPhone)
    {
        return $"Reminder: appointment {slot:MMM dd HH:mm}. Call {clinicPhone} to reschedule.";
    }
}

