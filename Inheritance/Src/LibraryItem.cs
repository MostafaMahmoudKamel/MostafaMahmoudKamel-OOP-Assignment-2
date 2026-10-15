namespace Inheritance;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }
    public int LoanPeriodDays { get; }
    public decimal LateFeeMultiplier { get; }
    public decimal BaseLateFee { get; private set; }
    public bool IsWithdrawn { get; private set; }
    public bool IsOnLoan { get; internal set; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        int loanPeriodDays,
        decimal baseLateFee,
        decimal lateFeeMultiplier)
    {
        CatalogNumber = RequireText(catalogNumber, nameof(catalogNumber));
        Title = RequireText(title, nameof(title));

        if (loanPeriodDays <= 0)
            throw new ArgumentOutOfRangeException(nameof(loanPeriodDays), "Loan period must be positive.");
        if (baseLateFee <= 0)
            throw new ArgumentOutOfRangeException(nameof(baseLateFee), "Base late fee must be greater than zero.");
        if (lateFeeMultiplier <= 0)
            throw new ArgumentOutOfRangeException(nameof(lateFeeMultiplier), "Late-fee multiplier must be positive.");

        LoanPeriodDays = loanPeriodDays;
        BaseLateFee = baseLateFee;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    public decimal GetDailyLateFee() => BaseLateFee * LateFeeMultiplier;

    internal void StartLoan()
    {
        if (IsWithdrawn)
            throw new InvalidOperationException($"'{Title}' is withdrawn and cannot be borrowed.");
        if (IsOnLoan)
            throw new InvalidOperationException($"'{Title}' is already on loan.");

        IsOnLoan = true;
    }

    internal void EndLoan()
    {
        IsOnLoan = false;
    }

    public void SetBaseLateFee(decimal newBaseLateFee)
    {
        if (newBaseLateFee <= 0)
            throw new ArgumentOutOfRangeException(nameof(newBaseLateFee), "New base late fee must be greater than zero.");

        BaseLateFee = newBaseLateFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan)
            throw new InvalidOperationException("An item currently on loan cannot be withdrawn.");
        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    private static string RequireText(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} cannot be null, empty, or whitespace.");

        return value;
    }
}
