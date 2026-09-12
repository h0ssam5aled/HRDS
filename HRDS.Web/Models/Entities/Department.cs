using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentCode { get; set; } = null!;

    public string DepartmentNameAr { get; set; } = null!;

    public string? DepartmentNameEn { get; set; }

    public int? CompanyId { get; set; }

    public int? CompanyBranchId { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int? ParentDepartmentId { get; set; }

    public byte DepartmentLevel { get; set; }

    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

    public virtual Department? ParentDepartment { get; set; }

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}
