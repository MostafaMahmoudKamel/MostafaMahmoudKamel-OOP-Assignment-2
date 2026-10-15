using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Course_EnrollmentDesk;

public class WelcomeService
{
    //send welcome message
    public string WelcomePacketMarkdown(CourseEnrollmentDesk course,string studentEmail, string studentName)
    {
        // Content design changes with academy marketing — not with seat algorithms.
        var status = course.Seated.Contains(studentEmail) ? "confirmed seat" : $"waitlist #{course.WaitlistPosition(studentEmail)}";
        return $"# Welcome to {course.CourseCode}\nHi {studentName},\nYour status: **{status}**.\n" +
               $"Bring a laptop. Discord onboarding link: https://example.invalid/{course.CourseCode.ToLowerInvariant()}\n";
    }
}
