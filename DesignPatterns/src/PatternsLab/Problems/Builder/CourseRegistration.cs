namespace PatternsLab.Problems.Builder;

public sealed class CourseRegistration
{
    public string StudentEmail { get; }
    public string CourseCode { get; }
    public string AccessMode { get; }
    public string? GroupCode { get; }
    public string? DiscountCode { get; }
    public bool SendWhatsApp { get; }
    public bool SendEmailWelcome { get; }
    public string? MentorNote { get; }
    public DateOnly? PreferredStart { get; }

    public CourseRegistration(
        string studentEmail,
        string courseCode,
        string accessMode,
        string? groupCode,
        string? discountCode,
        bool sendWhatsApp,
        bool sendEmailWelcome,
        string? mentorNote,
        DateOnly? preferredStart)
    {
        if (string.IsNullOrWhiteSpace(studentEmail)) throw new ArgumentException("email required");
        if (string.IsNullOrWhiteSpace(courseCode)) throw new ArgumentException("course required");

        if (accessMode == "LiveGroup" && string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("LiveGroup requires GroupCode");
        if (accessMode == "VideosOnly" && !string.IsNullOrWhiteSpace(groupCode))
            throw new InvalidOperationException("VideosOnly cannot have GroupCode");

        StudentEmail = studentEmail;
        CourseCode = courseCode;
        AccessMode = accessMode;
        GroupCode = groupCode;
        DiscountCode = discountCode;
        SendWhatsApp = sendWhatsApp;
        SendEmailWelcome = sendEmailWelcome;
        MentorNote = mentorNote;
        PreferredStart = preferredStart;
    }

    public override string ToString()
        => $"{StudentEmail} → {CourseCode} [{AccessMode}] group={GroupCode ?? "-"} discount={DiscountCode ?? "-"} wa={SendWhatsApp} mail={SendEmailWelcome}";
}

public class RegistrationCallSites
{
    private string _email;
    private string _courseCode;
    private string _groupCode;
    private string _accessMode;
    private string _discountCode;
    private bool _sendWhatsApp;
    private bool _sendEmailWelcome;
    private string _mentorNote;
    private DateOnly? _preferredStart;



    public RegistrationCallSites(string email, string courseCode)
    {
        _email = email;
        _courseCode = courseCode;
    }
    public RegistrationCallSites WithGroupCode(string groupCode)
    {
        _groupCode = groupCode;
        return this;
    }
    public RegistrationCallSites WithAccessMode(string accessMode)
    {
        _accessMode = accessMode;
        return this;
    }
    public RegistrationCallSites WithDiscountCode(string discountCode)
    {
        _discountCode = discountCode;
        return this;
    }
    public RegistrationCallSites WithSendWhatsApp(bool sendWhatsApp)
    {
        _sendWhatsApp = sendWhatsApp;
        return this;
    }
    public RegistrationCallSites WithSendEmailWelcome(bool sendEmailWelcome)
    {
        _sendEmailWelcome = sendEmailWelcome;
        return this;
    }
    public RegistrationCallSites WithMentorNote(string mentorNote)
    {
        _mentorNote = mentorNote;
        return this;
    }
    public RegistrationCallSites WithPreferredStart(DateOnly preferredStart)
    {
        _preferredStart = preferredStart;
        return this;
    }

    public CourseRegistration Build()
    {
        return new CourseRegistration(_email, _courseCode, _accessMode, _groupCode, _discountCode, 
            _sendWhatsApp, _sendEmailWelcome, _mentorNote, _preferredStart);
    }
}

//public static class RegistrationCallSites
//{
//    public static CourseRegistration CreateLiveStudentUgly()
//    {
//        return new CourseRegistration(
//            "sara@mail.com",
//            "SEF-101",
//            "LiveGroup",
//            "G1",
//            "EARLY10",
//            true,
//            true,
//            "Needs evening slot",
//            new DateOnly(2026, 10, 1));
//    }

//    public static CourseRegistration CreateVideosOnlyUgly()
//    {
//        return new CourseRegistration(
//            "ali@mail.com",
//            "SEF-101",
//            "VideosOnly",
//            null,
//            null,
//            false,
//            true,
//            null,
//            null);
//    }
//}
