using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.AppointmentDesk_
{
    internal class SlotSearchService
    {
        public bool IsWithinBusinessHours(AppointmentDesk desk, DateTimeOffset when)
        {
            if (when.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday) return false;
            var t = TimeOnly.FromDateTime(when.DateTime);
            return t >= desk.Open && t.AddMinutes(desk.SlotMinutes) <= desk.Close;
        }

        public DateTimeOffset? FindNextSlot(AppointmentDesk desk, DateTimeOffset from, int searchHours)
        {
            var cursor = Align(from, desk.SlotMinutes);
            var end = from.AddHours(searchHours);
            while (cursor < end)
            {
                if (IsWithinBusinessHours(desk, cursor) && !desk.BookedSlots.Contains(cursor))
                    return cursor;
                cursor = cursor.AddMinutes(desk.SlotMinutes);
            }
            return null;
        }

        private DateTimeOffset Align(DateTimeOffset from, int slotMinutes)
        {
            var minutes = from.Minute - (from.Minute % slotMinutes);
            return new DateTimeOffset(from.Year, from.Month, from.Day, from.Hour, minutes, 0, from.Offset);
        }
    }
}
