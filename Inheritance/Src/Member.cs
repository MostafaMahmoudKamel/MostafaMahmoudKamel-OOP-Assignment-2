namespace Inheritance;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public int MaximumLoans { get; }
    public decimal DiscountPercentage { get; }
    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maximumLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maximumLoans <= 0)
            throw new ArgumentOutOfRangeException(nameof(maximumLoans), "Maximum loans must be positive.");
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentOutOfRangeException(nameof(discountPercentage), "Discount must be between 0 and 100 percent.");

        MaximumLoans = maximumLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan BorrowItem(string loanId, LibraryItem item, DateTime borrowDate)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        int activeLoans = 0;
        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= MaximumLoans)
            throw new InvalidOperationException($"{FullName} cannot borrow more than {MaximumLoans} items at once.");

        item.StartLoan();
        try
        {
            Loan loan = new Loan(loanId, borrowDate, this, item);
            _loans.Add(loan);
            return loan;
        }
        catch
        {
            item.EndLoan();
            throw;
        }
    }

    internal void RecordReturnedLoan(Loan loan)
    {
        // Reading points are computed from the history; no points are manually assigned.
        // This method intentionally does not change the loan collection.
    }

    public int CountReturnedLoans()
    {
        int count = 0;
        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Returned)
                count++;
        }

        return count;
    }
}
