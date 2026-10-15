namespace Inheritance;

public sealed class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(string personId, string fullName, string phone, DateTime hireDate, decimal monthlySalary, string section)
        : base(personId, fullName, phone, hireDate, monthlySalary)
    {
        Section = RequireText(section, nameof(section));
    }

    public void Reassign(string newSection)
    {
        Section = RequireText(newSection, nameof(newSection));
    }
}
