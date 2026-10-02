using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string? EmployeeOldCode { get; set; }

    public string FirstNameAr { get; set; } = null!;

    public string? MiddleNameAr { get; set; }

    public string LastNameAr { get; set; } = null!;

    public string FirstNameEn { get; set; } = null!;

    public string? MiddleNameEn { get; set; }

    public string LastNameEn { get; set; } = null!;

    public int? GenderId { get; set; }

    public int? ReligionId { get; set; }

    public int? MaritalStatusId { get; set; }

    public int? NationalityId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? MilitaryStatusId { get; set; }

    public string? DriverLicenseNumber { get; set; }

    public string? PassportNumber { get; set; }

    public string? NationalIdNo { get; set; }

    public bool IsActive { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<AttendanceLog> AttendanceLogs { get; set; } = new List<AttendanceLog>();

    public virtual ICollection<BusinessMissionRequest> BusinessMissionRequests { get; set; } = new List<BusinessMissionRequest>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual EmergencyContact? EmergencyContact { get; set; }

    public virtual ICollection<EmployeeAllowance> EmployeeAllowances { get; set; } = new List<EmployeeAllowance>();

    public virtual ICollection<EmployeeAssetAssignment> EmployeeAssetAssignments { get; set; } = new List<EmployeeAssetAssignment>();

    public virtual ICollection<EmployeeBankAccount> EmployeeBankAccounts { get; set; } = new List<EmployeeBankAccount>();

    public virtual ICollection<EmployeeBonuse> EmployeeBonuses { get; set; } = new List<EmployeeBonuse>();

    public virtual ICollection<EmployeeDeduction> EmployeeDeductions { get; set; } = new List<EmployeeDeduction>();

    public virtual ICollection<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; } = new List<EmployeeLeaveBalance>();

    public virtual ICollection<EmployeeOvertimeRequest> EmployeeOvertimeRequests { get; set; } = new List<EmployeeOvertimeRequest>();

    public virtual ICollection<EmployeePenalty> EmployeePenalties { get; set; } = new List<EmployeePenalty>();

    public virtual EmployeePosition? EmployeePosition { get; set; }

    public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; } = new List<EmployeeQualification>();

    public virtual ICollection<EmployeeRemoteWorkRequest> EmployeeRemoteWorkRequests { get; set; } = new List<EmployeeRemoteWorkRequest>();

    public virtual ICollection<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; } = new List<EmployeeSalaryDetail>();

    public virtual EmployeeSalaryHistory? EmployeeSalaryHistory { get; set; }

    public virtual ICollection<EmployeeWorkSchedule> EmployeeWorkSchedules { get; set; } = new List<EmployeeWorkSchedule>();

    public virtual EmployeesDatum? EmployeesDatum { get; set; }

    public virtual ICollection<EmploymentHistory> EmploymentHistoryDirectManagers { get; set; } = new List<EmploymentHistory>();

    public virtual ICollection<EmploymentHistory> EmploymentHistoryEmployees { get; set; } = new List<EmploymentHistory>();

    public virtual ICollection<LeaveRequestApproval> LeaveRequestApprovals { get; set; } = new List<LeaveRequestApproval>();

    public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<LeaveRequest> LeaveRequestSubstituteEmployees { get; set; } = new List<LeaveRequest>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual ICollection<PermissionRequest> PermissionRequests { get; set; } = new List<PermissionRequest>();

    public virtual ProbationPeriod? ProbationPeriod { get; set; }

    public virtual ICollection<SafetyIncident> SafetyIncidents { get; set; } = new List<SafetyIncident>();

    public virtual ICollection<WorkflowStepsConfig> WorkflowStepsConfigs { get; set; } = new List<WorkflowStepsConfig>();
}
