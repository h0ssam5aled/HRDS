using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class EmployeeQualification
{
    public int EmployeeQualificationId { get; set; }

    public int EmployeeId { get; set; }

    public int QualificationId { get; set; }

    public int? InstitutionId { get; set; }

    public int? FacultyId { get; set; }

    public int? MajorId { get; set; }

    public short? GraduationYear { get; set; }

    public int? GradeOrGpa { get; set; }

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual AcademicFaculty? Faculty { get; set; }

    public virtual EducationGrade? GradeOrGpaNavigation { get; set; }

    public virtual EducationalInstitution? Institution { get; set; }

    public virtual AcademicMajor? Major { get; set; }

    public virtual EducationQualification Qualification { get; set; } = null!;
}
