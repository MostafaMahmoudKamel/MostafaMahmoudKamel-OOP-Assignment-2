using System;
using System.Collections.Generic;
using System.Text;

namespace SRP.src.SrpLab.Course_EnrollmentDesk;

public class PaymetCourseService
{
    //calculate values of course
    public string TuitionInvoiceLine(CourseEnrollmentDesk course,string studentEmail)
    {
        // Finance formatting / tax later — separate from enrollment capacity.
        if (!course.Seated.Contains(studentEmail)) return $"{course.CourseCode},WAITLIST,0.00";
        var vat = Math.Round(course.Tuition * 0.14m, 2);
        return $"{course.CourseCode},TUITION,{course.Tuition:0.00},VAT,{vat:0.00},TOTAL,{(course.Tuition + vat):0.00}";
    }
}
