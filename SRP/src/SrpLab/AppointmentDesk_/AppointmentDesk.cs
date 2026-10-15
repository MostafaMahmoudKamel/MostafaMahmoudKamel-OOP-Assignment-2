namespace SRP.src.SrpLab.AppointmentDesk_;

/// <summary>
/// Appointment desk: opening-hours rules, slot search, and ICS calendar blob generation.
/// </summary>
public sealed class AppointmentDesk
{
    private readonly HashSet<DateTimeOffset> _booked = new();
    public IReadOnlySet<DateTimeOffset> BookedSlots => _booked;
    public TimeOnly Open { get; }
    public TimeOnly Close { get; }
    public int SlotMinutes { get; }

    public AppointmentDesk(TimeOnly open, TimeOnly close, int slotMinutes)
    {
        Open = open;
        Close = close;
        SlotMinutes = slotMinutes;
    }









    public bool TryBook(DateTimeOffset slot, bool isWithinHours)
    {
        if (!isWithinHours || _booked.Contains(slot)) return false;
        _booked.Add(slot);
        return true;
    }






}
