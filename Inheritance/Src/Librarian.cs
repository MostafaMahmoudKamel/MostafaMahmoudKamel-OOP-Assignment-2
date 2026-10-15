namespace Inheritance;

public sealed class Librarian : Staff
{
    public Librarian(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary)
        : base(personId, fullName, phone, hireDate, monthlySalary)
    {
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan is null) throw new ArgumentNullException(nameof(loan));
        loan.Return(returnDate);
    }

    public void MarkItemLost(Loan loan)
    {
        if (loan is null) throw new ArgumentNullException(nameof(loan));
        loan.MarkAsLost();
    }
}
