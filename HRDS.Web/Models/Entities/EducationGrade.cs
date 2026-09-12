using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class EducationGrade
{
    public int GradeId { get; set; }

    public string GradeCode { get; set; } = null!;

    public string GradeNameAr { get; set; } = null!;

    public string? GradeNameEn { get; set; }

    public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; } = new List<EmployeeQualification>();
}
