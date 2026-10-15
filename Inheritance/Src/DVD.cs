namespace Inheritance;

public sealed class DVD : LibraryItem
{
    public DVD(string catalogNumber, string title, decimal baseLateFee)
        : base(catalogNumber, title, 7, baseLateFee, 2m)
    {
    }
}
