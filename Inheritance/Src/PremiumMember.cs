namespace Inheritance;

public sealed class PremiumMember : Member
{
    public int ReadingPoints => CountReturnedLoans() * 5;

    public PremiumMember(
        string personId,
        string fullName,
        string phone,
        decimal discountPercentage)
        : base(personId, fullName, phone, 10, discountPercentage)
    {
    }
}
