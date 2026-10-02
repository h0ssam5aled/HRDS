using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HRDS.Web.Areas.HR.ViewModels
{
    public class EmployeeViewModel
    {
        // ==========================================
        // 1. البيانات الأساسية (جدول Employee)
        // ==========================================
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = null!;
        public string? EmployeeOldCode { get; set; }

        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        public string FirstNameAr { get; set; } = null!;
        public string? MiddleNameAr { get; set; }
        [Required(ErrorMessage = "اسم العائلة مطلوب")]
        public string LastNameAr { get; set; } = null!;

        public string? FirstNameEn { get; set; }
        public string? MiddleNameEn { get; set; }
        public string? LastNameEn { get; set; }

        public int? GenderId { get; set; }
        public int? ReligionId { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? NationalityId { get; set; }
        public int? MilitaryStatusId { get; set; }

        public DateOnly? DateOfBirth { get; set; }
        public string? NationalIdNo { get; set; }
        public string? PassportNumber { get; set; }
        public string? DriverLicenseNumber { get; set; }

        public bool IsActive { get; set; } = true;

        // أسماء للعرض فقط (للجداول أو التفاصيل)
        public string? FullNameAr => $"{FirstNameAr} {MiddleNameAr} {LastNameAr}".Replace("  ", " ").Trim();
        public string? FullNameEn => $"{FirstNameEn} {MiddleNameEn} {LastNameEn}".Replace("  ", " ").Trim();

        // ==========================================
        // 2. بيانات الاتصال والعنوان (جدول EmployeesDatum)
        // ==========================================
        public int? CountryId { get; set; }
        public int? GovernorateId { get; set; }
        public int? CityId { get; set; }

        public string? EmployeeAddress { get; set; }

        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string? Email { get; set; }

        public string? FirstPhoneNo { get; set; }
        public string? SecondPhoneNo { get; set; }
        public string? FirstMobileNo { get; set; }
        public string? SecondMobileNo { get; set; }

        // ==========================================
        // 3. جهات الاتصال للطوارئ (جدول EmergencyContact)
        // ==========================================
        public string? ContactName { get; set; }
        public string? Relationship { get; set; }
        public string? EmergencyPhoneNumber { get; set; }
        public string? EmergencyAlternativePhone { get; set; }
        public string? EmergencyMobileNumber { get; set; }
        public string? EmergencyAlternativeMobileNo { get; set; }
        public bool IsPrimaryContact { get; set; } = true;
        public string? EmergencyNotes { get; set; }

        // ==========================================
        // 4. البيانات الوظيفية (جدول EmploymentHistory)
        // ==========================================
        public int? DirectManagerId { get; set; }
        public int? EmployeeStatusId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SectionId { get; set; }
        public int? JobTitleId { get; set; }
        public int? JobLevelId { get; set; }
        public int? CostCenterId { get; set; }
        public int? EmploymentTypeId { get; set; }
        public DateOnly? HireDate { get; set; }
        public DateOnly? TerminationDate { get; set; }
        public string? ResonOfLeaving { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyBranchId { get; set; }

        // ==========================================
        // 5. المنصب الوظيفي (جدول EmployeePosition)
        // ==========================================
        public int? PositionId { get; set; }
        public bool PrimaryPosition { get; set; } = true;
        public DateOnly? PositionFromDate { get; set; }
        public DateOnly? PositionToDate { get; set; }
        public int? AssignmentReasonId { get; set; }

        // ==========================================
        // 6. المؤهلات العلمية (جدول EmployeeQualification)
        // ==========================================
        public int? QualificationId { get; set; }
        public int? EducationalInstitutionId { get; set; }
        public int? FacultyId { get; set; }
        public int? MajorId { get; set; }
        public short? GraduationYear { get; set; }
        public int? GradeOrGpaId { get; set; }
        public string? QualificationNotes { get; set; }

        // ==========================================
        // 7. الحساب البنكي (جدول EmployeeBankAccount)
        // ==========================================
        public int? BankId { get; set; }
        public int? BankBranchId { get; set; }
        public string? AccountNumber { get; set; }
        public int? EmployeeBankAccountTypeId { get; set; }
        public string? Iban { get; set; }
        public int? CurrencyId { get; set; }
        public bool IsPrimaryBankAccount { get; set; } = true;

        // ==========================================
        // 8. مواعيد العمل والوردية (جدول EmployeeWorkSchedule)
        // ==========================================
        public int? ShiftId { get; set; }
        public int? ShiftPatternId { get; set; }
        public byte ScheduleType { get; set; } = 1; // 1 = Fixed Shift, 2 = Pattern / Rotating
        public DateOnly? ScheduleEffectiveFrom { get; set; }
        public DateOnly? ScheduleEffectiveTo { get; set; }
        public byte? SchedulePriority { get; set; } = 1;
        public string? ScheduleRemarks { get; set; }

        // ==========================================
        // 9. مستندات الموظف (جدول Document)
        // ==========================================
        public class EmployeeDocumentInputModel
        {
            public int? DocumentId { get; set; } // إضافة الحقل لمعالجة التعديل
            public int? DocumentTypeId { get; set; }
            public string? DocumentNumber { get; set; }
            public DateOnly? DocumentIssueDate { get; set; }
            public DateOnly? DocumentExpiryDate { get; set; }
            public IFormFile? DocumentFile { get; set; }
            public string? ExistingFilePath { get; set; } // إضافة الحقل لحفظ مسار الملف الحالي
            public bool IsDocumentMandatory { get; set; } = false;
            public string? DocumentNotes { get; set; }
        }

        public List<EmployeeDocumentInputModel> Documents { get; set; } = new();

        // ==========================================
        // 10. فترة التجربة (جدول ProbationPeriod)
        // ==========================================
        public DateOnly? ProbationStartDate { get; set; }
        public DateOnly? ProbationEndDate { get; set; }
        public bool IsProbationConfirmed { get; set; } = false;
        public DateOnly? ProbationConfirmationDate { get; set; }
        public int? ProbationDecisionBy { get; set; }
        public string? ProbationNotes { get; set; }

        // ==========================================
        // 11. تفاصيل الراتب (جدول EmployeeSalaryHistory)
        // ==========================================
        public decimal? BasicSalary { get; set; }
        public decimal? NetSalary { get; set; }
        public int? SalaryCurrencyId { get; set; }
        public DateOnly? SalaryFromDate { get; set; }
        public DateOnly? SalaryToDate { get; set; }
        public string? SalaryNotes { get; set; }

        // ==========================================
        // 12. البدلات والاستقطاعات (EmployeeAllowance & EmployeeDeduction)
        // ==========================================
        public class EmployeeAllowanceInputModel
        {
            public int? AllowanceId { get; set; } // إضافة الحقل لمعالجة التعديل
            public int? AllowanceTypeId { get; set; }
            public decimal? Amount { get; set; }
            public DateOnly? FromDate { get; set; }
            public DateOnly? ToDate { get; set; }
            public string? Notes { get; set; }
        }

        public class EmployeeDeductionInputModel
        {
            public int? DeductionId { get; set; } // إضافة الحقل لمعالجة التعديل
            public int? DeductionTypeId { get; set; }
            public decimal? Amount { get; set; }
            public DateOnly? FromDate { get; set; }
            public DateOnly? ToDate { get; set; }
            public string? Notes { get; set; }
        }

        public List<EmployeeAllowanceInputModel> Allowances { get; set; } = new();
        public List<EmployeeDeductionInputModel> Deductions { get; set; } = new();
    }
}