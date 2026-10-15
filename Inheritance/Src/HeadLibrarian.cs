namespace Inheritance;

public sealed class HeadLibrarian : Staff
{
    public HeadLibrarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary, 400m)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newBaseLateFee)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        item.SetBaseLateFee(newBaseLateFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        if (item is null) throw new ArgumentNullException(nameof(item));
        item.Restore();
    }
}
