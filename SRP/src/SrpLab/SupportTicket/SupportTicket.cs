namespace SRP.src.SrpLab.SupportTicket;

/// <summary>
/// Support ticket: SLA clocks, 
/// NLP-ish priority from free text,
/// and public reply templates.
/// </summary>
public sealed class SupportTicket
{
    public string Id { get; }
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public DateTimeOffset OpenedAt { get; }
    public string Priority { get; private set; } = "P3";

    public SupportTicket(string id, string subject, string body, DateTimeOffset openedAt
        )
    {
        Id = id;
        Subject = subject;
        Body = body;
        OpenedAt = openedAt;

    }

    public void AppendCustomerMessage(string text)
    {
        Body += "\n---\n" + text;
    }




}
