namespace Inheritance;

public class Person
{
    public string PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(string personId, string fullName, string phone)
    {
        PersonId = RequireText(personId, nameof(personId));
        FullName = RequireText(fullName, nameof(fullName));
        Phone = RequireText(phone, nameof(phone));
    }

    protected static string RequireText(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{fieldName} cannot be null, empty, or whitespace.");

        return value;
    }
}
