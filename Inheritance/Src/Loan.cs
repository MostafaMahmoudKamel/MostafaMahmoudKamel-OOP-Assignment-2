namespace Inheritance;

public sealed class Loan
{
    public string LoanId { get; }
    public DateTime BorrowDate { get; }
    public DateTime DueDate => BorrowDate.AddDays(Item.LoanPeriodDays);
    public Member Member { get; }
    public LibraryItem Item { get; }
    public LoanStatus Status { get; private set; }
    public DateTime? ReturnDate { get; private set; }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate is null)
                return 0m;

            int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;
            if (daysLate <= 0)
                return 0m;

            decimal grossFee = daysLate * Item.GetDailyLateFee();
            return grossFee * (100m - Member.DiscountPercentage) / 100m;
        }
    }

    internal Loan(string loanId, DateTime borrowDate, Member member, LibraryItem item)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID cannot be null, empty, or whitespace.", nameof(loanId));
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (item is null) throw new ArgumentNullException(nameof(item));

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a borrowed loan can be returned.");

        if (returnDate.Date < BorrowDate.Date)
            throw new ArgumentException("Return date cannot be earlier than the borrow date.", nameof(returnDate));

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.EndLoan();
        Member.RecordReturnedLoan(this);
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only a currently borrowed loan can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.EndLoan();
    }
}
