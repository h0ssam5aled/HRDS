using HRDS.Web.Areas.HR.ViewModels;
using HRDS.Web.Models;
using HRDS.Web.Models.Entities;
using HRDS.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HRDS.Web.Areas.HR.ViewModels.EmployeeViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace HRDS.Web.Areas.HR.Controllers
{
    [Authorize]
    [Area("HR")]
    [HasModuleAccess("HR")]
    public class EmployeesController : Controller
    {
        private readonly HRDSContext _context;
        private readonly IWebHostEnvironment _environment;

        public EmployeesController(HRDSContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var employees = await _context.Employees
                .Where(e => !e.IsDeleted)
                .Select(e => new
                {
                    employeeId = e.EmployeeId,
                    employeeCode = e.EmployeeCode,
                    fullNameAr = (e.FirstNameAr + " " + (e.MiddleNameAr ?? "") + " " + e.LastNameAr).Replace("  ", " ").Trim(),
                    fullNameEn = (e.FirstNameEn + " " + (e.MiddleNameEn ?? "") + " " + e.LastNameEn).Replace("  ", " ").Trim(),
                    nationalIdNo = e.NationalIdNo,
                    isActive = e.IsActive
                })
                .ToListAsync();

            return Json(new { data = employees });
        }

        // GET: HR/Employees/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateLookupsAsync();
            var model = new EmployeeViewModel { IsActive = true, IsPrimaryContact = true };
            return View(model);
        }

        // POST: HR/Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            ValidateEmployeeDates(model);
            ValidateGraduationYear(model);
            ValidateNationalId(model);

            if (!ModelState.IsValid)
            {
                await PopulateLookupsAsync(model.CountryId, model.GovernorateId);
                return View(model);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable))
            {
                try
                {
                    // 1. توليد كود الموظف التلقائي أوتوماتيكياً
                    var maxCode = await _context.Employees
                        .IgnoreQueryFilters()
                        .Select(e => e.EmployeeCode)
                        .Where(c => c.StartsWith("EMP-"))
                        .OrderByDescending(c => c)
                        .FirstOrDefaultAsync();

                    int nextNumber = 1;
                    if (!string.IsNullOrEmpty(maxCode) && maxCode.Length > 4)
                    {
                        if (int.TryParse(maxCode.Substring(4), out int currentNum))
                        {
                            nextNumber = currentNum + 1;
                        }
                    }

                    string generatedCode = $"EMP-{nextNumber:D6}";

                    // 2. حفظ البيانات الأساسية (جدول Employee)
                    var employeeEntity = new Employee
                    {
                        EmployeeCode = generatedCode,
                        EmployeeOldCode = model.EmployeeOldCode?.Trim(),
                        FirstNameAr = model.FirstNameAr.Trim(),
                        MiddleNameAr = model.MiddleNameAr?.Trim(),
                        LastNameAr = model.LastNameAr.Trim(),
                        FirstNameEn = model.FirstNameEn?.Trim(),
                        MiddleNameEn = model.MiddleNameEn?.Trim(),
                        LastNameEn = model.LastNameEn?.Trim(),
                        GenderId = model.GenderId,
                        ReligionId = model.ReligionId,
                        MaritalStatusId = model.MaritalStatusId,
                        NationalityId = model.NationalityId,
                        MilitaryStatusId = model.MilitaryStatusId,
                        DateOfBirth = model.DateOfBirth,
                        NationalIdNo = model.NationalIdNo?.Trim(),
                        PassportNumber = model.PassportNumber?.Trim(),
                        DriverLicenseNumber = model.DriverLicenseNumber?.Trim(),
                        IsActive = model.IsActive,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    _context.Employees.Add(employeeEntity);
                    await _context.SaveChangesAsync();

                    // 3. حفظ بيانات الاتصال والعنوان (جدول EmployeesDatum)
                    var employeeDatumEntity = new EmployeesDatum
                    {
                        EmployeeId = employeeEntity.EmployeeId,
                        CountryId = model.CountryId,
                        GovernorateId = model.GovernorateId,
                        CityId = model.CityId,
                        EmployeeAddress = model.EmployeeAddress?.Trim(),
                        Email = model.Email?.Trim(),
                        FirstPhoneNo = model.FirstPhoneNo?.Trim(),
                        SecondPhoneNo = model.SecondPhoneNo?.Trim(),
                        FirstMobileNo = model.FirstMobileNo?.Trim(),
                        SecondMobileNo = model.SecondMobileNo?.Trim(),
                        IsActive = model.IsActive,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    _context.EmployeesData.Add(employeeDatumEntity);

                    // 4. حفظ بيانات الطوارئ (جدول EmergencyContact) في حالة إدخال اسم الجهة
                    if (!string.IsNullOrWhiteSpace(model.ContactName))
                    {
                        var emergencyContactEntity = new EmergencyContact
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            ContactName = model.ContactName.Trim(),
                            Relationship = model.Relationship?.Trim(),
                            PhoneNumber = model.EmergencyPhoneNumber?.Trim(),
                            AlternativePhone = model.EmergencyAlternativePhone?.Trim(),
                            MobileNumber = model.EmergencyMobileNumber?.Trim(),
                            AlternativeMobileNo = model.EmergencyAlternativeMobileNo?.Trim(),
                            IsPrimary = model.IsPrimaryContact,
                            Notes = model.EmergencyNotes?.Trim(),
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmergencyContacts.Add(emergencyContactEntity);
                    }

                    // 5. حفظ البيانات الوظيفية (جدول EmploymentHistory)
                    if (model.DepartmentId.HasValue && model.JobTitleId.HasValue && model.EmploymentTypeId.HasValue && model.HireDate.HasValue)
                    {
                        var historyEntity = new EmploymentHistory
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            DirectManagerId = model.DirectManagerId,
                            EmployeeStatusId = model.EmployeeStatusId ?? 1,
                            DepartmentId = model.DepartmentId.Value,
                            SectionId = model.SectionId,
                            JobTitleId = model.JobTitleId.Value,
                            JobLevelId = model.JobLevelId,
                            CostCenterId = model.CostCenterId,
                            EmploymentTypeId = model.EmploymentTypeId.Value,
                            HireDate = model.HireDate.Value,
                            TerminationDate = model.TerminationDate,
                            ResonOfLeaving = model.ResonOfLeaving?.Trim(),
                            CompanyId = model.CompanyId,
                            CompanyBranchId = model.CompanyBranchId,
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmploymentHistories.Add(historyEntity);
                    }

                    // 6. حفظ المنصب الوظيفي (جدول EmployeePosition)
                    if (model.PositionId.HasValue && model.PositionFromDate.HasValue)
                    {
                        var positionEntity = new EmployeePosition
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            PositionId = model.PositionId.Value,
                            PrimaryPosition = model.PrimaryPosition,
                            FromDate = model.PositionFromDate.Value,
                            ToDate = model.PositionToDate,
                            AssignmentReasonId = model.AssignmentReasonId,
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmployeePositions.Add(positionEntity);
                    }

                    // 7. حفظ المؤهلات العلمية (جدول EmployeeQualification)
                    if (model.QualificationId.HasValue)
                    {
                        var qualificationEntity = new EmployeeQualification
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            QualificationId = model.QualificationId.Value,
                            InstitutionId = model.EducationalInstitutionId,
                            FacultyId = model.FacultyId,
                            MajorId = model.MajorId,
                            GraduationYear = model.GraduationYear,
                            // ملاحظة: تم ربطه بـ GradeOrGpa حسب الـ Navigation Property في الكيان لديك
                            // إذا كان العمود في الجدول ينقل الـ Foreign Key الخاص بالتقدير
                            Notes = model.QualificationNotes?.Trim(),
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmployeeQualifications.Add(qualificationEntity);
                    }

                    // 8. حفظ الحساب البنكي (جدول EmployeeBankAccount)
                    if (model.BankId.HasValue && !string.IsNullOrWhiteSpace(model.AccountNumber))
                    {
                        var bankAccountEntity = new EmployeeBankAccount
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            BankId = model.BankId.Value,
                            BranchId = model.BankBranchId,
                            AccountNumber = model.AccountNumber.Trim(),
                            EmployeeBankAccountTypeId = model.EmployeeBankAccountTypeId,
                            Iban = model.Iban?.Trim(),
                            CurrencyId = model.CurrencyId,
                            IsPrimary = model.IsPrimaryBankAccount,
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmployeeBankAccounts.Add(bankAccountEntity);
                    }

                    // 9. حفظ جدولة وراديات العمل (جدول EmployeeWorkSchedule)
                    if (model.ScheduleEffectiveFrom.HasValue && (model.ShiftId.HasValue || model.ShiftPatternId.HasValue))
                    {
                        var scheduleEntity = new EmployeeWorkSchedule
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            ShiftId = model.ShiftId,
                            PatternId = model.ShiftPatternId,
                            ScheduleType = model.ScheduleType,
                            EffectiveFrom = model.ScheduleEffectiveFrom.Value,
                            EffectiveTo = model.ScheduleEffectiveTo,
                            Priority = model.SchedulePriority,
                            Remarks = model.ScheduleRemarks?.Trim(),
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmployeeWorkSchedules.Add(scheduleEntity);
                    }

                    // 10. حفظ قائمة المستندات والأوراق الثبوتية (جدول Document)
                    if (model.Documents != null && model.Documents.Any())
                    {
                        foreach (var doc in model.Documents)
                        {
                            // يتجاوز السطر إذا لم يتم اختيار نوع المستند
                            if (!doc.DocumentTypeId.HasValue) continue;

                            string? uploadedFilePath = null;

                            if (doc.DocumentFile != null && doc.DocumentFile.Length > 0)
                            {
                                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "documents");
                                if (!Directory.Exists(uploadsFolder))
                                {
                                    Directory.CreateDirectory(uploadsFolder);
                                }

                                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(doc.DocumentFile.FileName)}";
                                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await doc.DocumentFile.CopyToAsync(stream);
                                }

                                uploadedFilePath = Path.Combine("uploads", "documents", uniqueFileName).Replace("\\", "/");
                            }

                            var documentEntity = new Document
                            {
                                EmployeeId = employeeEntity.EmployeeId,
                                DocumentTypeId = doc.DocumentTypeId.Value,
                                DocumentNumber = doc.DocumentNumber?.Trim(),
                                IssueDate = doc.DocumentIssueDate,
                                ExpiryDate = doc.DocumentExpiryDate,
                                FilePath = "/" + uploadedFilePath,
                                IsMandatory = doc.IsDocumentMandatory,
                                Notes = doc.DocumentNotes?.Trim(),
                                IsActive = model.IsActive,
                                CreatedAt = DateTime.Now,
                                IsDeleted = false
                            };

                            _context.Documents.Add(documentEntity);
                        }
                    }

                    // 11. حفظ فترة التجربة (جدول ProbationPeriod)
                    if (model.ProbationStartDate.HasValue && model.ProbationEndDate.HasValue)
                    {
                        var probationEntity = new ProbationPeriod
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            StartDate = model.ProbationStartDate.Value,
                            EndDate = model.ProbationEndDate.Value,
                            IsConfirmed = model.IsProbationConfirmed,
                            ConfirmationDate = model.ProbationConfirmationDate,
                            DecisionBy = model.ProbationDecisionBy,
                            Notes = model.ProbationNotes?.Trim(),
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.ProbationPeriods.Add(probationEntity);
                    }

                    // 12. حفظ تفاصيل الراتب (جدول EmployeeSalaryHistory)
                    if (model.BasicSalary.HasValue && model.SalaryFromDate.HasValue)
                    {
                        var salaryEntity = new EmployeeSalaryHistory
                        {
                            EmployeeId = employeeEntity.EmployeeId,
                            BasicSalary = model.BasicSalary.Value,
                            NetSalary = model.NetSalary ?? model.BasicSalary.Value,
                            CurrencyId = model.SalaryCurrencyId,
                            FromDate = model.SalaryFromDate.Value,
                            ToDate = model.SalaryToDate,
                            Notes = model.SalaryNotes?.Trim(),
                            IsActive = model.IsActive,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false
                        };

                        _context.EmployeeSalaryHistories.Add(salaryEntity);
                    }

                    // 13. حفظ البدلات (جدول EmployeeAllowance)
                    if (model.Allowances != null && model.Allowances.Any())
                    {
                        foreach (var item in model.Allowances)
                        {
                            if (!item.AllowanceTypeId.HasValue || !item.FromDate.HasValue) continue;

                            var allowanceEntity = new EmployeeAllowance
                            {
                                EmployeeId = employeeEntity.EmployeeId,
                                AllowanceTypeId = item.AllowanceTypeId.Value,
                                Amount = item.Amount.Value,
                                FromDate = item.FromDate.Value,
                                ToDate = item.ToDate,
                                Notes = item.Notes?.Trim(),
                                IsActive = model.IsActive,
                                CreatedAt = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.EmployeeAllowances.Add(allowanceEntity);
                        }
                    }

                    // 14. حفظ الاستقطاعات (جدول EmployeeDeduction)
                    if (model.Deductions != null && model.Deductions.Any())
                    {
                        foreach (var item in model.Deductions)
                        {
                            if (!item.DeductionTypeId.HasValue || !item.FromDate.HasValue) continue;

                            var deductionEntity = new EmployeeDeduction
                            {
                                EmployeeId = employeeEntity.EmployeeId,
                                DeductionTypeId = item.DeductionTypeId.Value,
                                Amount = item.Amount.Value,
                                FromDate = item.FromDate.Value,
                                ToDate = item.ToDate,
                                Notes = item.Notes?.Trim(),
                                IsActive = model.IsActive,
                                CreatedAt = DateTime.Now,
                                IsDeleted = false
                            };
                            _context.EmployeeDeductions.Add(deductionEntity);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar"
                    ? "تم إنشاء بيانات الموظف بنجاح." : "Employee has been created successfully.";


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "حدث خطأ أثناء حفظ البيانات، يرجى إعادة المحاولة.");
                    await PopulateLookupsAsync(model.CountryId, model.GovernorateId);
                    return View(model);
                }
            }
        }

        // AJAX Endpoints
        [HttpGet]
        public async Task<IActionResult> GetGovernorates(int countryId)
        {
            var isArabic = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "ar";
            var govs = await _context.Governorates
                .Where(g => g.CountryId == countryId)
                .Select(g => new
                {
                    id = g.GovernorateId,
                    name = isArabic ? g.GovernorateNameAr : g.GovernorateNameEn
                })
                .ToListAsync();

            return Json(govs);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int governorateId)
        {
            var isArabic = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "ar";
            var cities = await _context.Cities
                .Where(c => c.GovernorateId == governorateId)
                .Select(c => new
                {
                    id = c.CityId,
                    name = isArabic ? c.CityNameAr : c.CityNameEn
                })
                .ToListAsync();

            return Json(cities);
        }

        private async Task PopulateLookupsAsync(int? selectedCountryId = null, int? selectedGovId = null)
        {
            var isArabic = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "ar";

            // 1. البيانات الشخصية والمكانية
            ViewBag.Genders = new SelectList(await _context.Genders.ToListAsync() ?? new(), nameof(Gender.GenderId), isArabic ? nameof(Gender.GenderNameAr) : nameof(Gender.GenderNameEn));
            ViewBag.Religions = new SelectList(await _context.Religions.ToListAsync() ?? new(), nameof(Religion.ReligionId), isArabic ? nameof(Religion.ReligionNameAr) : nameof(Religion.ReligionNameEn));
            ViewBag.MaritalStatuses = new SelectList(await _context.MaritalStatuses.ToListAsync() ?? new(), nameof(MaritalStatus.MaritalStatusId), isArabic ? nameof(MaritalStatus.MaritalStatusNameAr) : nameof(MaritalStatus.MaritalStatusNameEn));
            ViewBag.Nationalities = new SelectList(await _context.Nationalities.ToListAsync() ?? new(), nameof(Nationality.NationalityId), isArabic ? nameof(Nationality.NationalityNameAr) : nameof(Nationality.NationalityNameEn));
            ViewBag.MilitaryStatuses = new SelectList(await _context.MilitaryStatuses.ToListAsync() ?? new(), nameof(MilitaryStatus.MilitaryStatusId), isArabic ? nameof(MilitaryStatus.MilitaryStatusNameAr) : nameof(MilitaryStatus.MilitaryStatusNameEn));

            ViewBag.Countries = new SelectList(await _context.Countries.ToListAsync() ?? new(), nameof(Country.CountryId), isArabic ? nameof(Country.CountryNameAr) : nameof(Country.CountryNameEn));

            ViewBag.Governorates = selectedCountryId.HasValue
                ? new SelectList(await _context.Governorates.Where(x => x.CountryId == selectedCountryId).ToListAsync() ?? new(), nameof(Governorate.GovernorateId), isArabic ? nameof(Governorate.GovernorateNameAr) : nameof(Governorate.GovernorateNameEn))
                : new SelectList(Enumerable.Empty<SelectListItem>());

            ViewBag.Cities = selectedGovId.HasValue
                ? new SelectList(await _context.Cities.Where(x => x.GovernorateId == selectedGovId).ToListAsync() ?? new(), nameof(City.CityId), isArabic ? nameof(City.CityNameAr) : nameof(City.CityNameEn))
                : new SelectList(Enumerable.Empty<SelectListItem>());

            // 2. البيانات الوظيفية (مع حماية nameof المباشرة)
            ViewBag.EmployeeStatuses = new SelectList(await _context.EmployeeStatuses.ToListAsync() ?? new(), nameof(EmployeeStatus.EmployeeStatusId), isArabic ? nameof(EmployeeStatus.EmployeeStatusNameAr) : nameof(EmployeeStatus.EmployeeStatusNameEn));
            ViewBag.Departments = new SelectList(await _context.Departments.Where(d => !d.IsDeleted).ToListAsync() ?? new(), nameof(Department.DepartmentId), isArabic ? nameof(Department.DepartmentNameAr) : nameof(Department.DepartmentNameEn));
            ViewBag.JobTitles = new SelectList(await _context.JobTitles.Where(j => !j.IsDeleted).ToListAsync() ?? new(), nameof(JobTitle.JobTitleId), isArabic ? nameof(JobTitle.JobTitleNameAr) : nameof(JobTitle.JobTitleNameEn));
            ViewBag.JobLevels = new SelectList(await _context.JobLevels.ToListAsync() ?? new(), nameof(JobLevel.JobLevelId), isArabic ? nameof(JobLevel.JobLevelNameAr) : nameof(JobLevel.JobLevelNameEn));
            ViewBag.EmploymentTypes = new SelectList(await _context.EmploymentTypes.ToListAsync() ?? new(), nameof(EmploymentType.EmploymentTypeId), isArabic ? nameof(EmploymentType.EmploymentTypeNameAr) : nameof(EmploymentType.EmploymentTypeNameEn));

            // 3. المدير المباشر (تجميع الأسماء بأمان)
            var managers = await _context.Employees
                .Where(e => !e.IsDeleted)
                .Select(e => new
                {
                    e.EmployeeId,
                    Name = isArabic
                        ? ((e.FirstNameAr ?? "") + " " + (e.LastNameAr ?? "")).Trim()
                        : ((e.FirstNameEn ?? "") + " " + (e.LastNameEn ?? "")).Trim()
                })
                .ToListAsync() ?? new();

            ViewBag.DirectManagers = new SelectList(managers, "EmployeeId", "Name");

            ViewBag.Positions = new SelectList(await _context.Positions.Where(p => !p.IsDeleted).ToListAsync() ?? new(), nameof(Position.PositionId), isArabic ? nameof(Position.PositionNameAr) : nameof(Position.PositionNameEn));
            ViewBag.AssignmentReasons = new SelectList(await _context.AssignmentReasons.ToListAsync() ?? new(), nameof(AssignmentReason.AssignmentReasonId), isArabic ? nameof(AssignmentReason.AssignmentReasonNameAr) : nameof(AssignmentReason.AssignmentReasonNameEn));

            ViewBag.Qualifications = new SelectList(await _context.EducationQualifications.ToListAsync() ?? new(), nameof(EducationQualification.QualificationId), isArabic ? nameof(EducationQualification.QualificationNameAr) : nameof(EducationQualification.QualificationNameEn));
            ViewBag.EducationalInstitutions = new SelectList(await _context.EducationalInstitutions.ToListAsync() ?? new(), nameof(EducationalInstitution.InstitutionId), isArabic ? nameof(EducationalInstitution.InstitutionNameAr) : nameof(EducationalInstitution.InstitutionNameEn));
            ViewBag.Faculties = new SelectList(await _context.AcademicFaculties.ToListAsync() ?? new(), nameof(AcademicFaculty.FacultyId), isArabic ? nameof(AcademicFaculty.FacultyNameAr) : nameof(AcademicFaculty.FacultyNameEn));
            ViewBag.Majors = new SelectList(await _context.AcademicMajors.ToListAsync() ?? new(), nameof(AcademicMajor.MajorId), isArabic ? nameof(AcademicMajor.MajorNameAr) : nameof(AcademicMajor.MajorNameEn));
            ViewBag.EducationGrades = new SelectList(await _context.EducationGrades.ToListAsync() ?? new(), nameof(EducationGrade.GradeId), isArabic ? nameof(EducationGrade.GradeNameAr) : nameof(EducationGrade.GradeNameEn));

            ViewBag.Banks = new SelectList(await _context.Banks.ToListAsync() ?? new(), nameof(Bank.BankId), isArabic ? nameof(Bank.BankNameAr) : nameof(Bank.BankNameEn));
            ViewBag.BankBranches = new SelectList(await _context.BankBranches.ToListAsync() ?? new(), nameof(BankBranch.BranchId), isArabic ? nameof(BankBranch.BankBranchNameAr) : nameof(BankBranch.BankBranchNameEn));
            ViewBag.BankAccountTypes = new SelectList(await _context.BankAccountTypes.ToListAsync() ?? new(), nameof(BankAccountType.BankAccountTypeId), isArabic ? nameof(BankAccountType.BankAccountTypeNameAr) : nameof(BankAccountType.BankAccountTypeNameEn));
            ViewBag.Currencies = new SelectList(await _context.Currencies.ToListAsync() ?? new(), nameof(Currency.CurrencyId), isArabic ? nameof(Currency.CurrencyNameAr) : nameof(Currency.CurrencyNameEn));

            ViewBag.Shifts = new SelectList(await _context.Shifts.ToListAsync() ?? new(), nameof(Shift.ShiftId), isArabic ? nameof(Shift.ShiftNameAr) : nameof(Shift.ShiftNameEn));
            ViewBag.ShiftPatterns = new SelectList(await _context.ShiftPatterns.ToListAsync() ?? new(), nameof(ShiftPattern.PatternId), isArabic ? nameof(ShiftPattern.PatternNameAr) : nameof(ShiftPattern.PatternNameEn));

            ViewBag.DocumentTypes = new SelectList(await _context.DocumentTypes.ToListAsync() ?? new(), nameof(DocumentType.DocumentTypeId), isArabic ? nameof(DocumentType.TypeNameAr) : nameof(DocumentType.TypeNameEn));

            ViewBag.DecisionMakers = ViewBag.DirectManagers; // إعادة استخدام قائمة الموظفين/المدراء المجهزة سابقاً

            ViewBag.SalaryCurrencies = ViewBag.Currencies; // أو تعيينها من _context.Currencies مباشرة  

            ViewBag.AllowanceTypes = new SelectList(await _context.AllowanceTypes.ToListAsync() ?? new(), nameof(AllowanceType.AllowanceTypeId), isArabic ? nameof(AllowanceType.AllowanceTypeNameAr) : nameof(AllowanceType.AllowanceTypeNameEn));
            ViewBag.DeductionTypes = new SelectList(await _context.DeductionTypes.ToListAsync() ?? new(), nameof(DeductionType.DeductionTypeId), isArabic ? nameof(DeductionType.DeductionTypeNameAr) : nameof(DeductionType.DeductionTypeNameEn));
        }

        // GET: HR/Employees/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                .Include(e => e.EmployeesDatum)
                .Include(e => e.EmergencyContact)
                .Include(e => e.EmploymentHistoryEmployees) // تم تعديل اسمها بناءً على الـ Entity
                .Include(e => e.EmployeePosition)           // علاقة 1-to-1 مفرد
                .Include(e => e.EmployeeQualifications)
                .Include(e => e.EmployeeBankAccounts)
                .Include(e => e.EmployeeWorkSchedules)
                .Include(e => e.Documents)
                .Include(e => e.ProbationPeriod)            // علاقة 1-to-1 مفرد
                .Include(e => e.EmployeeSalaryHistory)      // علاقة 1-to-1 مفرد
                .Include(e => e.EmployeeAllowances)
                .Include(e => e.EmployeeDeductions)
                .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);

            if (employee == null) return NotFound();

            // جلب بيانات العلاقات المفردة (1-to-1)
            var datum = employee.EmployeesDatum;
            var emergency = employee.EmergencyContact;
            var position = employee.EmployeePosition;
            var probation = employee.ProbationPeriod;
            var salary = employee.EmployeeSalaryHistory;

            // جلب السجل الوظيفي من القائمة الخاصة بالموظف
            var history = employee.EmploymentHistoryEmployees.FirstOrDefault(x => !x.IsDeleted);

            // جلب أحدث سجل في القوائم المتبقية
            var qualification = employee.EmployeeQualifications.FirstOrDefault(x => !x.IsDeleted);
            var bankAccount = employee.EmployeeBankAccounts.FirstOrDefault(x => !x.IsDeleted);
            var schedule = employee.EmployeeWorkSchedules.FirstOrDefault(x => !x.IsDeleted);

            var model = new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                EmployeeOldCode = employee.EmployeeOldCode,
                FirstNameAr = employee.FirstNameAr,
                MiddleNameAr = employee.MiddleNameAr,
                LastNameAr = employee.LastNameAr,
                FirstNameEn = employee.FirstNameEn,
                MiddleNameEn = employee.MiddleNameEn,
                LastNameEn = employee.LastNameEn,
                GenderId = employee.GenderId,
                ReligionId = employee.ReligionId,
                MaritalStatusId = employee.MaritalStatusId,
                NationalityId = employee.NationalityId,
                MilitaryStatusId = employee.MilitaryStatusId,
                DateOfBirth = employee.DateOfBirth,
                NationalIdNo = employee.NationalIdNo,
                PassportNumber = employee.PassportNumber,
                DriverLicenseNumber = employee.DriverLicenseNumber,
                IsActive = employee.IsActive,

                // البيانات الشخصية والاتصال (1-to-1)
                CountryId = datum?.CountryId,
                GovernorateId = datum?.GovernorateId,
                CityId = datum?.CityId,
                EmployeeAddress = datum?.EmployeeAddress,
                Email = datum?.Email,
                FirstPhoneNo = datum?.FirstPhoneNo,
                SecondPhoneNo = datum?.SecondPhoneNo,
                FirstMobileNo = datum?.FirstMobileNo,
                SecondMobileNo = datum?.SecondMobileNo,

                // جهات الاتصال للطوارئ (1-to-1)
                ContactName = emergency?.ContactName,
                Relationship = emergency?.Relationship,
                EmergencyPhoneNumber = emergency?.PhoneNumber,
                EmergencyAlternativePhone = emergency?.AlternativePhone,
                EmergencyMobileNumber = emergency?.MobileNumber,
                EmergencyAlternativeMobileNo = emergency?.AlternativeMobileNo,
                IsPrimaryContact = emergency?.IsPrimary ?? true,
                EmergencyNotes = emergency?.Notes,

                // البيانات الوظيفية (من قائمة EmploymentHistoryEmployees)
                DepartmentId = history?.DepartmentId,
                SectionId = history?.SectionId,
                JobTitleId = history?.JobTitleId,
                JobLevelId = history?.JobLevelId,
                CostCenterId = history?.CostCenterId,
                EmploymentTypeId = history?.EmploymentTypeId,
                EmployeeStatusId = history?.EmployeeStatusId,
                DirectManagerId = history?.DirectManagerId,
                HireDate = history?.HireDate,
                TerminationDate = history?.TerminationDate,
                ResonOfLeaving = history?.ResonOfLeaving,
                CompanyId = history?.CompanyId,
                CompanyBranchId = history?.CompanyBranchId,

                // المنصب الوظيفي (1-to-1)
                PositionId = position?.PositionId,
                PrimaryPosition = position?.PrimaryPosition ?? true,
                PositionFromDate = position?.FromDate,
                PositionToDate = position?.ToDate,
                AssignmentReasonId = position?.AssignmentReasonId,

                // المؤهل العلمي
                // استبدل الجزء الخاص بالمؤهل العلمي بـ:
                QualificationId = qualification?.QualificationId,
                EducationalInstitutionId = qualification?.InstitutionId,
                FacultyId = qualification?.FacultyId,
                MajorId = qualification?.MajorId,
                GraduationYear = qualification?.GraduationYear,
                //GradeOrGpaId = qualification?.GradeOrGpa.HasValue == true ? (int?)Math.Round(qualification.GradeOrGpa.Value) : null,
                GradeOrGpaId = qualification?.GradeOrGpa,
                QualificationNotes = qualification?.Notes,

                // الحساب البنكي
                BankId = bankAccount?.BankId,
                BankBranchId = bankAccount?.BranchId,
                AccountNumber = bankAccount?.AccountNumber,
                EmployeeBankAccountTypeId = bankAccount?.EmployeeBankAccountTypeId,
                Iban = bankAccount?.Iban,
                CurrencyId = bankAccount?.CurrencyId,
                IsPrimaryBankAccount = bankAccount?.IsPrimary ?? true,

                // مواعيد العمل
                ShiftId = schedule?.ShiftId,
                ShiftPatternId = schedule?.PatternId,
                ScheduleType = schedule?.ScheduleType ?? 1,
                ScheduleEffectiveFrom = schedule?.EffectiveFrom,
                ScheduleEffectiveTo = schedule?.EffectiveTo,
                SchedulePriority = schedule?.Priority,
                ScheduleRemarks = schedule?.Remarks,

                // المستندات والوثائق
                Documents = employee.Documents.Where(d => !d.IsDeleted).Select(d => new EmployeeViewModel.EmployeeDocumentInputModel
                {
                    DocumentId = d.DocumentId,
                    DocumentTypeId = d.DocumentTypeId,
                    DocumentNumber = d.DocumentNumber,
                    DocumentIssueDate = d.IssueDate,
                    DocumentExpiryDate = d.ExpiryDate,
                    ExistingFilePath = d.FilePath,
                    IsDocumentMandatory = d.IsMandatory,
                    DocumentNotes = d.Notes
                }).ToList(),

                // فترة التجربة (1-to-1)
                ProbationStartDate = probation?.StartDate,
                ProbationEndDate = probation?.EndDate,
                IsProbationConfirmed = probation?.IsConfirmed ?? false,
                ProbationConfirmationDate = probation?.ConfirmationDate,
                ProbationDecisionBy = probation?.DecisionBy,
                ProbationNotes = probation?.Notes,

                // تفاصيل الراتب (1-to-1)
                BasicSalary = salary?.BasicSalary,
                NetSalary = salary?.NetSalary,
                SalaryCurrencyId = salary?.CurrencyId,
                SalaryFromDate = salary?.FromDate,
                SalaryToDate = salary?.ToDate,
                SalaryNotes = salary?.Notes,

                // البدلات والاستقطاعات
                Allowances = employee.EmployeeAllowances.Where(a => !a.IsDeleted).Select(a => new EmployeeViewModel.EmployeeAllowanceInputModel
                {
                    AllowanceId = a.EmployeeAllowanceId,
                    AllowanceTypeId = a.AllowanceTypeId,
                    Amount = a.Amount,
                    FromDate = a.FromDate,
                    ToDate = a.ToDate,
                    Notes = a.Notes
                }).ToList(),

                Deductions = employee.EmployeeDeductions.Where(d => !d.IsDeleted).Select(d => new EmployeeViewModel.EmployeeDeductionInputModel
                {
                    DeductionId = d.EmployeeDeductionId,
                    DeductionTypeId = d.DeductionTypeId,
                    Amount = d.Amount,
                    FromDate = d.FromDate,
                    ToDate = d.ToDate,
                    Notes = d.Notes
                }).ToList()
            };

            await PopulateLookupsAsync(model.CountryId, model.GovernorateId);
            return View(model);
        }

        private void ValidateEmployeeDates(EmployeeViewModel model)
        {
            var isArabic =
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar";

            const int minimumWorkingAge = 18;

            // =========================================================
            // 1. Date of Birth / Hire Date
            // =========================================================

            if (model.DateOfBirth.HasValue &&
                model.HireDate.HasValue)
            {
                var birthDate = model.DateOfBirth.Value;
                var hireDate = model.HireDate.Value;

                // Hire Date cannot be before Date of Birth
                if (hireDate < birthDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ التعيين قبل تاريخ الميلاد."
                        : "Hire date cannot be earlier than date of birth.";

                    ModelState.AddModelError(
                        nameof(model.HireDate),
                        message);
                }

                // Minimum working age
                var age = hireDate.Year - birthDate.Year;

                if (birthDate > hireDate.AddYears(-age))
                {
                    age--;
                }

                if (age < minimumWorkingAge)
                {
                    var message = isArabic
                        ? "يجب ألا يقل عمر الموظف عن 18 سنة في تاريخ التعيين."
                        : "The employee must be at least 18 years old on the hire date.";

                    ModelState.AddModelError(
                        nameof(model.DateOfBirth),
                        message);
                }
            }


            // =========================================================
            // 2. Hire Date / Termination Date
            // =========================================================

            if (model.HireDate.HasValue &&
                model.TerminationDate.HasValue)
            {
                var hireDate = model.HireDate.Value;
                var terminationDate = model.TerminationDate.Value;

                if (terminationDate < hireDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ انتهاء الخدمة قبل تاريخ التعيين."
                        : "Termination date cannot be earlier than hire date.";

                    ModelState.AddModelError(
                        nameof(model.TerminationDate),
                        message);
                }
            }


            // =========================================================
            // 3. Position
            // =========================================================

            if (model.PositionFromDate.HasValue)
            {
                var fromDate = model.PositionFromDate.Value;

                // Position cannot start before Hire Date
                if (model.HireDate.HasValue &&
                    fromDate < model.HireDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية الوظيفة قبل تاريخ التعيين."
                        : "Position start date cannot be earlier than hire date.";

                    ModelState.AddModelError(
                        nameof(model.PositionFromDate),
                        message);
                }

                // Position cannot start after Termination Date
                if (model.TerminationDate.HasValue &&
                    fromDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية الوظيفة بعد تاريخ انتهاء الخدمة."
                        : "Position start date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.PositionFromDate),
                        message);
                }
            }

            if (model.PositionFromDate.HasValue &&
                model.PositionToDate.HasValue)
            {
                var fromDate = model.PositionFromDate.Value;
                var toDate = model.PositionToDate.Value;

                // ToDate cannot be before FromDate
                if (toDate < fromDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية الوظيفة قبل تاريخ بداية الوظيفة."
                        : "Position end date cannot be earlier than position start date.";

                    ModelState.AddModelError(
                        nameof(model.PositionToDate),
                        message);
                }

                // ToDate cannot be after Termination Date
                if (model.TerminationDate.HasValue &&
                    toDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية الوظيفة بعد تاريخ انتهاء الخدمة."
                        : "Position end date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.PositionToDate),
                        message);
                }
            }


            // =========================================================
            // 4. Work Schedule
            // =========================================================

            if (model.ScheduleEffectiveFrom.HasValue)
            {
                var fromDate = model.ScheduleEffectiveFrom.Value;

                // Schedule cannot start before Hire Date
                if (model.HireDate.HasValue &&
                    fromDate < model.HireDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية جدول العمل قبل تاريخ التعيين."
                        : "Work schedule effective date cannot be earlier than hire date.";

                    ModelState.AddModelError(
                        nameof(model.ScheduleEffectiveFrom),
                        message);
                }

                // Schedule cannot start after Termination Date
                if (model.TerminationDate.HasValue &&
                    fromDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية جدول العمل بعد تاريخ انتهاء الخدمة."
                        : "Work schedule effective date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.ScheduleEffectiveFrom),
                        message);
                }
            }

            if (model.ScheduleEffectiveFrom.HasValue &&
                model.ScheduleEffectiveTo.HasValue)
            {
                var fromDate = model.ScheduleEffectiveFrom.Value;
                var toDate = model.ScheduleEffectiveTo.Value;

                // ToDate cannot be before FromDate
                if (toDate < fromDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية جدول العمل قبل تاريخ بدايته."
                        : "Work schedule end date cannot be earlier than its start date.";

                    ModelState.AddModelError(
                        nameof(model.ScheduleEffectiveTo),
                        message);
                }

                // ToDate cannot be after Termination Date
                if (model.TerminationDate.HasValue &&
                    toDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية جدول العمل بعد تاريخ انتهاء الخدمة."
                        : "Work schedule end date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.ScheduleEffectiveTo),
                        message);
                }
            }


            // =========================================================
            // 5. Probation Period
            // =========================================================

            if (model.ProbationStartDate.HasValue)
            {
                var startDate = model.ProbationStartDate.Value;

                // Probation cannot start before Hire Date
                if (model.HireDate.HasValue &&
                    startDate < model.HireDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن تبدأ فترة الاختبار قبل تاريخ التعيين."
                        : "Probation cannot start before the hire date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationStartDate),
                        message);
                }

                // Probation cannot start after Termination Date
                if (model.TerminationDate.HasValue &&
                    startDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن تبدأ فترة الاختبار بعد تاريخ انتهاء الخدمة."
                        : "Probation cannot start after the termination date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationStartDate),
                        message);
                }
            }

            if (model.ProbationStartDate.HasValue &&
                model.ProbationEndDate.HasValue)
            {
                var startDate = model.ProbationStartDate.Value;
                var endDate = model.ProbationEndDate.Value;

                // End Date cannot be before Start Date
                if (endDate < startDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ انتهاء فترة الاختبار قبل تاريخ بدايتها."
                        : "Probation end date cannot be earlier than probation start date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationEndDate),
                        message);
                }

                // End Date cannot be after Termination Date
                if (model.TerminationDate.HasValue &&
                    endDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ انتهاء فترة الاختبار بعد تاريخ انتهاء الخدمة."
                        : "Probation end date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationEndDate),
                        message);
                }
            }


            // =========================================================
            // 6. Confirmation Date
            // =========================================================

            if (model.ProbationConfirmationDate.HasValue)
            {
                var confirmationDate = model.ProbationConfirmationDate.Value;

                // Confirmation cannot be before Hire Date
                if (model.HireDate.HasValue &&
                    confirmationDate < model.HireDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ التثبيت قبل تاريخ التعيين."
                        : "Confirmation date cannot be earlier than hire date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationConfirmationDate),
                        message);
                }

                // Confirmation cannot be before Probation Start
                if (model.ProbationStartDate.HasValue &&
                    confirmationDate < model.ProbationStartDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ التثبيت قبل تاريخ بداية فترة الاختبار."
                        : "Confirmation date cannot be earlier than probation start date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationConfirmationDate),
                        message);
                }

                // Confirmation cannot be after Probation End
                if (model.ProbationEndDate.HasValue &&
                    confirmationDate > model.ProbationEndDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ التثبيت بعد تاريخ انتهاء فترة الاختبار."
                        : "Confirmation date cannot be later than probation end date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationConfirmationDate),
                        message);
                }

                // Confirmation cannot be after Termination
                if (model.TerminationDate.HasValue &&
                    confirmationDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ التثبيت بعد تاريخ انتهاء الخدمة."
                        : "Confirmation date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.ProbationConfirmationDate),
                        message);
                }
            }


            // =========================================================
            // 7. Salary
            // =========================================================

            if (model.SalaryFromDate.HasValue)
            {
                var fromDate = model.SalaryFromDate.Value;

                // Salary cannot start before Hire Date
                if (model.HireDate.HasValue &&
                    fromDate < model.HireDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية الراتب قبل تاريخ التعيين."
                        : "Salary start date cannot be earlier than hire date.";

                    ModelState.AddModelError(
                        nameof(model.SalaryFromDate),
                        message);
                }

                // Salary cannot start after Termination Date
                if (model.TerminationDate.HasValue &&
                    fromDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ بداية الراتب بعد تاريخ انتهاء الخدمة."
                        : "Salary start date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.SalaryFromDate),
                        message);
                }
            }

            if (model.SalaryFromDate.HasValue &&
                model.SalaryToDate.HasValue)
            {
                var fromDate = model.SalaryFromDate.Value;
                var toDate = model.SalaryToDate.Value;

                // ToDate cannot be before FromDate
                if (toDate < fromDate)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية الراتب قبل تاريخ بداية الراتب."
                        : "Salary end date cannot be earlier than salary start date.";

                    ModelState.AddModelError(
                        nameof(model.SalaryToDate),
                        message);
                }

                // ToDate cannot be after Termination Date
                if (model.TerminationDate.HasValue &&
                    toDate > model.TerminationDate.Value)
                {
                    var message = isArabic
                        ? "لا يمكن أن يكون تاريخ نهاية الراتب بعد تاريخ انتهاء الخدمة."
                        : "Salary end date cannot be later than termination date.";

                    ModelState.AddModelError(
                        nameof(model.SalaryToDate),
                        message);
                }
            }


            // =========================================================
            // 8. Allowances
            // =========================================================

            if (model.Allowances != null)
            {
                foreach (var item in model.Allowances)
                {
                    if (item.FromDate.HasValue)
                    {
                        var fromDate = item.FromDate.Value;

                        // Allowance cannot start before Hire Date
                        if (model.HireDate.HasValue &&
                            fromDate < model.HireDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ بداية البدل قبل تاريخ التعيين."
                                : "Allowance start date cannot be earlier than hire date.";

                            ModelState.AddModelError(
                                nameof(model.Allowances),
                                message);
                        }

                        // Allowance cannot start after Termination Date
                        if (model.TerminationDate.HasValue &&
                            fromDate > model.TerminationDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ بداية البدل بعد تاريخ انتهاء الخدمة."
                                : "Allowance start date cannot be later than termination date.";

                            ModelState.AddModelError(
                                nameof(model.Allowances),
                                message);
                        }
                    }

                    if (item.FromDate.HasValue &&
                        item.ToDate.HasValue)
                    {
                        var fromDate = item.FromDate.Value;
                        var toDate = item.ToDate.Value;

                        // ToDate cannot be before FromDate
                        if (toDate < fromDate)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ نهاية البدل قبل تاريخ بدايته."
                                : "Allowance end date cannot be earlier than allowance start date.";

                            ModelState.AddModelError(
                                nameof(model.Allowances),
                                message);
                        }

                        // ToDate cannot be after Termination Date
                        if (model.TerminationDate.HasValue &&
                            toDate > model.TerminationDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ نهاية البدل بعد تاريخ انتهاء الخدمة."
                                : "Allowance end date cannot be later than termination date.";

                            ModelState.AddModelError(
                                nameof(model.Allowances),
                                message);
                        }
                    }
                }
            }


            // =========================================================
            // 9. Deductions
            // =========================================================

            if (model.Deductions != null)
            {
                foreach (var item in model.Deductions)
                {
                    if (item.FromDate.HasValue)
                    {
                        var fromDate = item.FromDate.Value;

                        // Deduction cannot start before Hire Date
                        if (model.HireDate.HasValue &&
                            fromDate < model.HireDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ بداية الخصم قبل تاريخ التعيين."
                                : "Deduction start date cannot be earlier than hire date.";

                            ModelState.AddModelError(
                                nameof(model.Deductions),
                                message);
                        }

                        // Deduction cannot start after Termination Date
                        if (model.TerminationDate.HasValue &&
                            fromDate > model.TerminationDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ بداية الخصم بعد تاريخ انتهاء الخدمة."
                                : "Deduction start date cannot be later than termination date.";

                            ModelState.AddModelError(
                                nameof(model.Deductions),
                                message);
                        }
                    }

                    if (item.FromDate.HasValue &&
                        item.ToDate.HasValue)
                    {
                        var fromDate = item.FromDate.Value;
                        var toDate = item.ToDate.Value;

                        // ToDate cannot be before FromDate
                        if (toDate < fromDate)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ نهاية الخصم قبل تاريخ بدايته."
                                : "Deduction end date cannot be earlier than deduction start date.";

                            ModelState.AddModelError(
                                nameof(model.Deductions),
                                message);
                        }

                        // ToDate cannot be after Termination Date
                        if (model.TerminationDate.HasValue &&
                            toDate > model.TerminationDate.Value)
                        {
                            var message = isArabic
                                ? "لا يمكن أن يكون تاريخ نهاية الخصم بعد تاريخ انتهاء الخدمة."
                                : "Deduction end date cannot be later than termination date.";

                            ModelState.AddModelError(
                                nameof(model.Deductions),
                                message);
                        }
                    }
                }
            }
        }

        private void ValidateGraduationYear(EmployeeViewModel model)
        {
            var isArabic =
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar";

            if (!model.GraduationYear.HasValue)
                return;

            var graduationYear = model.GraduationYear.Value;
            var currentYear = DateTime.Today.Year;

            // لا يمكن أن تكون سنة التخرج في المستقبل
            if (graduationYear > currentYear)
            {
                var message = isArabic
                    ? "لا يمكن أن تكون سنة التخرج أكبر من السنة الحالية."
                    : "Graduation year cannot be greater than the current year.";

                ModelState.AddModelError(
                    nameof(model.GraduationYear),
                    message);
            }

            // لا يمكن أن تكون سنة التخرج قبل سنة الميلاد
            if (model.DateOfBirth.HasValue &&
                graduationYear < model.DateOfBirth.Value.Year)
            {
                var message = isArabic
                    ? "لا يمكن أن تكون سنة التخرج قبل سنة الميلاد."
                    : "Graduation year cannot be earlier than the year of birth.";

                ModelState.AddModelError(
                    nameof(model.GraduationYear),
                    message);
            }

            // التحقق من العمر وقت التخرج
            if (model.DateOfBirth.HasValue)
            {
                var ageAtGraduation =
                    graduationYear - model.DateOfBirth.Value.Year;

                if (ageAtGraduation < 18)
                {
                    var message = isArabic
                        ? "يجب ألا يكون عمر الموظف أقل من 18 سنة في سنة التخرج."
                        : "The employee must be at least 18 years old in the graduation year.";

                    ModelState.AddModelError(
                        nameof(model.GraduationYear),
                        message);
                }
            }
        }

        private bool IsValidEgyptianGovernorateCode(int code)
        {
            return code switch
            {
                01 => true, // القاهرة
                02 => true, // الإسكندرية
                03 => true, // بورسعيد
                04 => true, // السويس
                11 => true, // دمياط
                12 => true, // الدقهلية
                13 => true, // الشرقية
                14 => true, // القليوبية
                15 => true, // كفر الشيخ
                16 => true, // الغربية
                17 => true, // المنوفية
                18 => true, // البحيرة
                19 => true, // الإسماعيلية
                21 => true, // الجيزة
                22 => true, // بني سويف
                23 => true, // الفيوم
                24 => true, // المنيا
                25 => true, // أسيوط
                26 => true, // سوهاج
                27 => true, // قنا
                28 => true, // أسوان
                29 => true, // الأقصر
                31 => true, // البحر الأحمر
                32 => true, // الوادي الجديد
                33 => true, // مطروح
                34 => true, // شمال سيناء
                35 => true, // جنوب سيناء
                88 => true, // خارج الجمهورية
                _ => false
            };
        }

        private bool ValidateEgyptianNationalIdChecksum(string nationalId)
        {
            if (nationalId.Length != 14)
                return false;

            int sum = 0;

            for (int i = 0; i < 13; i++)
            {
                int digit = nationalId[i] - '0';

                int weight = i % 2 == 0 ? 1 : 2;

                int value = digit * weight;

                sum += (value / 10) + (value % 10);
            }

            int checkDigit = (10 - (sum % 10)) % 10;

            return checkDigit == (nationalId[13] - '0');
        }

        private void ValidateNationalId(EmployeeViewModel model)
        {
            var isArabic = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar";

            if (string.IsNullOrWhiteSpace(model.NationalIdNo))
                return;

            var nationalId = model.NationalIdNo.Trim();

            // =========================================================
            // 1. Length & Digits
            // =========================================================

            if (!Regex.IsMatch(nationalId, @"^\d{14}$"))
            {
                var message = isArabic
                    ? "الرقم القومي يجب أن يتكون من 14 رقمًا."
                    : "National ID must contain exactly 14 digits.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);

                return;
            }


            // =========================================================
            // 2. Century
            // =========================================================

            if (nationalId[0] != '2' && nationalId[0] != '3')
            {
                var message = isArabic
                    ? "الرقم القومي غير صحيح."
                    : "Invalid national ID.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);

                return;
            }


            // =========================================================
            // 3. Governorate Code
            // =========================================================

            var governorateCode =
                int.Parse(nationalId.Substring(7, 2));

            if (!IsValidEgyptianGovernorateCode(governorateCode))
            {
                var message = isArabic
                    ? "كود المحافظة في الرقم القومي غير صحيح."
                    : "Invalid governorate code in national ID.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);
            }


            // =========================================================
            // 4. Date of Birth inside National ID
            // =========================================================

            var century = nationalId[0] == '2' ? 1900 : 2000;

            var year = century + int.Parse(nationalId.Substring(1, 2));

            var month = int.Parse(nationalId.Substring(3, 2));

            var day = int.Parse(nationalId.Substring(5, 2));

            DateOnly nationalIdBirthDate;

            try
            {
                nationalIdBirthDate = new DateOnly(year, month, day);
            }
            catch
            {
                var message = isArabic
                    ? "تاريخ الميلاد الموجود في الرقم القومي غير صحيح."
                    : "The date of birth contained in the national ID is invalid.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);

                return;
            }


            // =========================================================
            // 5. Compare with Employee Date of Birth
            // =========================================================

            if (model.DateOfBirth.HasValue)
            {
                var employeeBirthDate = model.DateOfBirth.Value;

                if (nationalIdBirthDate != employeeBirthDate)
                {
                    var message = isArabic
                        ? "تاريخ الميلاد في الرقم القومي لا يطابق تاريخ ميلاد الموظف."
                        : "The date of birth in the national ID does not match the employee's date of birth.";

                    ModelState.AddModelError(nameof(model.NationalIdNo), message);
                }
            }


            // =========================================================
            // 6. National ID Birth Date cannot be in the future
            // =========================================================

            if (nationalIdBirthDate > DateOnly.FromDateTime(DateTime.Today))
            {
                var message = isArabic
                    ? "تاريخ الميلاد الموجود في الرقم القومي لا يمكن أن يكون في المستقبل."
                    : "The date of birth in the national ID cannot be in the future.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);
            }


            // =========================================================
            // 7. Checksum
            // =========================================================

            if (!ValidateEgyptianNationalIdChecksum(nationalId))
            {
                var message = isArabic
                    ? "الرقم القومي غير صحيح."
                    : "Invalid national ID.";

                ModelState.AddModelError(nameof(model.NationalIdNo), message);
            }
        }

        // POST: HR/Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (id != model.EmployeeId) return NotFound();

            ValidateEmployeeDates(model);
            ValidateGraduationYear(model);
            ValidateNationalId(model);

            if (!ModelState.IsValid)
            {
                await PopulateLookupsAsync(model.CountryId, model.GovernorateId);
                return View(model);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var currentTime = DateTime.Now;

                    var employeeEntity = await _context.Employees
                        .Include(e => e.EmployeesDatum)
                        .Include(e => e.EmergencyContact)
                        .Include(e => e.EmploymentHistoryEmployees)
                        .Include(e => e.EmployeePosition)
                        .Include(e => e.EmployeeQualifications)
                        .Include(e => e.EmployeeBankAccounts)
                        .Include(e => e.EmployeeWorkSchedules)
                        .Include(e => e.Documents)
                        .Include(e => e.ProbationPeriod)
                        .Include(e => e.EmployeeSalaryHistory)
                        .Include(e => e.EmployeeAllowances)
                        .Include(e => e.EmployeeDeductions)
                        .FirstOrDefaultAsync(e => e.EmployeeId == id && !e.IsDeleted);

                    if (employeeEntity == null) return NotFound();

                    // 1. Employee
                    employeeEntity.EmployeeOldCode = model.EmployeeOldCode?.Trim();
                    employeeEntity.FirstNameAr = model.FirstNameAr.Trim();
                    employeeEntity.MiddleNameAr = model.MiddleNameAr?.Trim();
                    employeeEntity.LastNameAr = model.LastNameAr.Trim();
                    employeeEntity.FirstNameEn = model.FirstNameEn?.Trim();
                    employeeEntity.MiddleNameEn = model.MiddleNameEn?.Trim();
                    employeeEntity.LastNameEn = model.LastNameEn?.Trim();
                    employeeEntity.GenderId = model.GenderId;
                    employeeEntity.ReligionId = model.ReligionId;
                    employeeEntity.MaritalStatusId = model.MaritalStatusId;
                    employeeEntity.NationalityId = model.NationalityId;
                    employeeEntity.MilitaryStatusId = model.MilitaryStatusId;
                    employeeEntity.DateOfBirth = model.DateOfBirth;
                    employeeEntity.NationalIdNo = model.NationalIdNo?.Trim();
                    employeeEntity.PassportNumber = model.PassportNumber?.Trim();
                    employeeEntity.DriverLicenseNumber = model.DriverLicenseNumber?.Trim();
                    employeeEntity.IsActive = model.IsActive;
                    employeeEntity.UpdatedAt = currentTime;

                    // 2. EmployeesDatum (1-to-1)
                    var datumEntity = employeeEntity.EmployeesDatum;
                    if (datumEntity == null)
                    {
                        datumEntity = new EmployeesDatum { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                        employeeEntity.EmployeesDatum = datumEntity;
                    }
                    datumEntity.CountryId = model.CountryId;
                    datumEntity.GovernorateId = model.GovernorateId;
                    datumEntity.CityId = model.CityId;
                    datumEntity.EmployeeAddress = model.EmployeeAddress?.Trim();
                    datumEntity.Email = model.Email?.Trim();
                    datumEntity.FirstPhoneNo = model.FirstPhoneNo?.Trim();
                    datumEntity.SecondPhoneNo = model.SecondPhoneNo?.Trim();
                    datumEntity.FirstMobileNo = model.FirstMobileNo?.Trim();
                    datumEntity.SecondMobileNo = model.SecondMobileNo?.Trim();
                    datumEntity.IsActive = model.IsActive;
                    datumEntity.UpdatedAt = currentTime;

                    // 3. EmergencyContact (1-to-1)
                    var emergencyEntity = employeeEntity.EmergencyContact;
                    if (!string.IsNullOrWhiteSpace(model.ContactName))
                    {
                        if (emergencyEntity == null)
                        {
                            emergencyEntity = new EmergencyContact { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmergencyContact = emergencyEntity;
                        }
                        emergencyEntity.ContactName = model.ContactName.Trim();
                        emergencyEntity.Relationship = model.Relationship?.Trim();
                        emergencyEntity.PhoneNumber = model.EmergencyPhoneNumber?.Trim();
                        emergencyEntity.AlternativePhone = model.EmergencyAlternativePhone?.Trim();
                        emergencyEntity.MobileNumber = model.EmergencyMobileNumber?.Trim();
                        emergencyEntity.AlternativeMobileNo = model.EmergencyAlternativeMobileNo?.Trim();
                        emergencyEntity.IsPrimary = model.IsPrimaryContact;
                        emergencyEntity.Notes = model.EmergencyNotes?.Trim();
                        emergencyEntity.IsActive = model.IsActive;
                        emergencyEntity.UpdatedAt = currentTime;
                    }

                    // 4. EmploymentHistory (من قائمة EmploymentHistoryEmployees)
                    if (model.DepartmentId.HasValue && model.JobTitleId.HasValue && model.EmploymentTypeId.HasValue && model.HireDate.HasValue)
                    {
                        var historyEntity = employeeEntity.EmploymentHistoryEmployees.FirstOrDefault(x => !x.IsDeleted);
                        if (historyEntity == null)
                        {
                            historyEntity = new EmploymentHistory { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmploymentHistoryEmployees.Add(historyEntity);
                        }
                        historyEntity.DirectManagerId = model.DirectManagerId;
                        historyEntity.EmployeeStatusId = model.EmployeeStatusId ?? 1;
                        historyEntity.DepartmentId = model.DepartmentId.Value;
                        historyEntity.SectionId = model.SectionId;
                        historyEntity.JobTitleId = model.JobTitleId.Value;
                        historyEntity.JobLevelId = model.JobLevelId;
                        historyEntity.CostCenterId = model.CostCenterId;
                        historyEntity.EmploymentTypeId = model.EmploymentTypeId.Value;
                        historyEntity.HireDate = model.HireDate.Value;
                        historyEntity.TerminationDate = model.TerminationDate;
                        historyEntity.ResonOfLeaving = model.ResonOfLeaving?.Trim();
                        historyEntity.CompanyId = model.CompanyId;
                        historyEntity.CompanyBranchId = model.CompanyBranchId;
                        historyEntity.IsActive = model.IsActive;
                        historyEntity.UpdatedAt = currentTime;
                    }

                    // 5. EmployeePosition (1-to-1)
                    if (model.PositionId.HasValue && model.PositionFromDate.HasValue)
                    {
                        var positionEntity = employeeEntity.EmployeePosition;
                        if (positionEntity == null)
                        {
                            positionEntity = new EmployeePosition { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmployeePosition = positionEntity;
                        }
                        positionEntity.PositionId = model.PositionId.Value;
                        positionEntity.PrimaryPosition = model.PrimaryPosition;
                        positionEntity.FromDate = model.PositionFromDate.Value;
                        positionEntity.ToDate = model.PositionToDate;
                        positionEntity.AssignmentReasonId = model.AssignmentReasonId;
                        positionEntity.IsActive = model.IsActive;
                        positionEntity.UpdatedAt = currentTime;
                    }

                    // 6. EmployeeQualification
                    if (model.QualificationId.HasValue)
                    {
                        var qualificationEntity = employeeEntity.EmployeeQualifications.FirstOrDefault(x => !x.IsDeleted);
                        if (qualificationEntity == null)
                        {
                            qualificationEntity = new EmployeeQualification { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmployeeQualifications.Add(qualificationEntity);
                        }
                        qualificationEntity.QualificationId = model.QualificationId.Value;
                        qualificationEntity.InstitutionId = model.EducationalInstitutionId;
                        qualificationEntity.FacultyId = model.FacultyId;
                        qualificationEntity.MajorId = model.MajorId;
                        qualificationEntity.GraduationYear = model.GraduationYear;
                        qualificationEntity.GradeOrGpa = model.GradeOrGpaId;
                        qualificationEntity.Notes = model.QualificationNotes?.Trim();
                        qualificationEntity.IsActive = model.IsActive;
                        qualificationEntity.UpdatedAt = currentTime;
                    }

                    // 7. EmployeeBankAccount
                    if (model.BankId.HasValue && !string.IsNullOrWhiteSpace(model.AccountNumber))
                    {
                        var bankEntity = employeeEntity.EmployeeBankAccounts.FirstOrDefault(x => !x.IsDeleted);
                        if (bankEntity == null)
                        {
                            bankEntity = new EmployeeBankAccount { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmployeeBankAccounts.Add(bankEntity);
                        }
                        bankEntity.BankId = model.BankId.Value;
                        bankEntity.BranchId = model.BankBranchId;
                        bankEntity.AccountNumber = model.AccountNumber.Trim();
                        bankEntity.EmployeeBankAccountTypeId = model.EmployeeBankAccountTypeId;
                        bankEntity.Iban = model.Iban?.Trim();
                        bankEntity.CurrencyId = model.CurrencyId;
                        bankEntity.IsPrimary = model.IsPrimaryBankAccount;
                        bankEntity.IsActive = model.IsActive;
                        bankEntity.UpdatedAt = currentTime;
                    }

                    // 8. EmployeeWorkSchedule
                    if (model.ScheduleEffectiveFrom.HasValue && (model.ShiftId.HasValue || model.ShiftPatternId.HasValue))
                    {
                        var scheduleEntity = employeeEntity.EmployeeWorkSchedules.FirstOrDefault(x => !x.IsDeleted);
                        if (scheduleEntity == null)
                        {
                            scheduleEntity = new EmployeeWorkSchedule { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmployeeWorkSchedules.Add(scheduleEntity);
                        }
                        scheduleEntity.ShiftId = model.ShiftId;
                        scheduleEntity.PatternId = model.ShiftPatternId;
                        scheduleEntity.ScheduleType = model.ScheduleType;
                        scheduleEntity.EffectiveFrom = model.ScheduleEffectiveFrom.Value;
                        scheduleEntity.EffectiveTo = model.ScheduleEffectiveTo;
                        scheduleEntity.Priority = model.SchedulePriority;
                        scheduleEntity.Remarks = model.ScheduleRemarks?.Trim();
                        scheduleEntity.IsActive = model.IsActive;
                        scheduleEntity.UpdatedAt = currentTime;
                    }

                    // 9. Documents

                    if (model.Documents != null)
                    {
                        // المستندات الموجودة حاليًا في الـ Model
                        var currentDocIds = model.Documents.Where(d => d.DocumentId.HasValue).Select(d => d.DocumentId!.Value).ToList();

                        // المستندات التي تم حذفها من الشاشة
                        var docsToRemove = employeeEntity.Documents.Where(d => !d.IsDeleted && !currentDocIds.Contains(d.DocumentId)).ToList();

                        foreach (var doc in docsToRemove)
                        {
                            doc.IsDeleted = true;
                            doc.DeletedAt = currentTime;
                            doc.UpdatedAt = currentTime;
                        }

                        // إضافة / تعديل المستندات
                        foreach (var doc in model.Documents)
                        {
                            // لا يوجد نوع مستند، نتجاهل السجل
                            if (!doc.DocumentTypeId.HasValue)
                                continue;

                            // في حالة عدم رفع ملف جديد، نحتفظ بالملف الحالي
                            string? uploadedFilePath = doc.ExistingFilePath;

                            // =========================================================
                            // رفع ملف جديد
                            // =========================================================
                            if (doc.DocumentFile != null && doc.DocumentFile.Length > 0)
                            {
                                // Physical Path
                                // المسار الفعلي على الجهاز أو السيرفر
                                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "documents");

                                if (!Directory.Exists(uploadsFolder))
                                {
                                    Directory.CreateDirectory(uploadsFolder);
                                }

                                // الحصول على امتداد الملف فقط
                                var extension = Path.GetExtension(doc.DocumentFile.FileName);

                                // اسم ملف Unique
                                var uniqueFileName = $"{Guid.NewGuid()}{extension}";

                                // Physical file path
                                var physicalFilePath = Path.Combine(uploadsFolder, uniqueFileName);

                                // حفظ الملف فعليًا
                                await using (var stream = new FileStream(physicalFilePath, FileMode.Create))
                                {
                                    await doc.DocumentFile.CopyToAsync(stream);
                                }

                                // =====================================================
                                // URL Path
                                // هذا هو المسار الذي يتم تخزينه في قاعدة البيانات
                                // =====================================================
                                uploadedFilePath = $"/uploads/documents/{uniqueFileName}";
                            }

                            // =========================================================
                            // تعديل مستند موجود
                            // =========================================================
                            if (doc.DocumentId.HasValue && doc.DocumentId.Value > 0)
                            {
                                var existingDoc = employeeEntity.Documents.FirstOrDefault(d => d.DocumentId == doc.DocumentId.Value);

                                if (existingDoc != null)
                                {
                                    existingDoc.DocumentTypeId = doc.DocumentTypeId.Value;
                                    existingDoc.DocumentNumber = doc.DocumentNumber?.Trim();
                                    existingDoc.IssueDate = doc.DocumentIssueDate;
                                    existingDoc.ExpiryDate = doc.DocumentExpiryDate;
                                    existingDoc.FilePath = uploadedFilePath;
                                    existingDoc.IsMandatory = doc.IsDocumentMandatory;
                                    existingDoc.Notes = doc.DocumentNotes?.Trim();
                                    existingDoc.IsActive = model.IsActive;
                                    existingDoc.UpdatedAt = currentTime;

                                    // في حالة كان المستند محذوف Soft Delete
                                    // وتم إرساله مرة أخرى
                                    existingDoc.IsDeleted = false;
                                    existingDoc.DeletedAt = null;
                                }
                            }
                            else
                            {
                                // =====================================================
                                // إضافة مستند جديد
                                // =====================================================
                                var newDoc = new Document
                                {
                                    EmployeeId = id,
                                    DocumentTypeId = doc.DocumentTypeId.Value,
                                    DocumentNumber = doc.DocumentNumber?.Trim(),
                                    IssueDate = doc.DocumentIssueDate,
                                    ExpiryDate = doc.DocumentExpiryDate,
                                    FilePath = uploadedFilePath,
                                    IsMandatory = doc.IsDocumentMandatory,
                                    Notes = doc.DocumentNotes?.Trim(),
                                    IsActive = model.IsActive,
                                    CreatedAt = currentTime,
                                    IsDeleted = false
                                };

                                employeeEntity.Documents.Add(newDoc);
                            }
                        }
                    }

                    // 10. ProbationPeriod (1-to-1)
                    if (model.ProbationStartDate.HasValue && model.ProbationEndDate.HasValue)
                    {
                        var probationEntity = employeeEntity.ProbationPeriod;
                        if (probationEntity == null)
                        {
                            probationEntity = new ProbationPeriod { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.ProbationPeriod = probationEntity;
                        }
                        probationEntity.StartDate = model.ProbationStartDate.Value;
                        probationEntity.EndDate = model.ProbationEndDate.Value;
                        probationEntity.IsConfirmed = model.IsProbationConfirmed;
                        probationEntity.ConfirmationDate = model.ProbationConfirmationDate;
                        probationEntity.DecisionBy = model.ProbationDecisionBy;
                        probationEntity.Notes = model.ProbationNotes?.Trim();
                        probationEntity.IsActive = model.IsActive;
                        probationEntity.UpdatedAt = currentTime;
                    }

                    // 11. EmployeeSalaryHistory (1-to-1)
                    if (model.BasicSalary.HasValue && model.SalaryFromDate.HasValue)
                    {
                        var salaryEntity = employeeEntity.EmployeeSalaryHistory;
                        if (salaryEntity == null)
                        {
                            salaryEntity = new EmployeeSalaryHistory { EmployeeId = id, CreatedAt = currentTime, IsDeleted = false };
                            employeeEntity.EmployeeSalaryHistory = salaryEntity;
                        }
                        salaryEntity.BasicSalary = model.BasicSalary.Value;
                        salaryEntity.NetSalary = model.NetSalary ?? model.BasicSalary.Value;
                        salaryEntity.CurrencyId = model.SalaryCurrencyId;
                        salaryEntity.FromDate = model.SalaryFromDate.Value;
                        salaryEntity.ToDate = model.SalaryToDate;
                        salaryEntity.Notes = model.SalaryNotes?.Trim();
                        salaryEntity.IsActive = model.IsActive;
                        salaryEntity.UpdatedAt = currentTime;
                    }

                    // 12. Allowances
                    if (model.Allowances != null)
                    {
                        var currentAllowanceIds = model.Allowances.Where(a => a.AllowanceId.HasValue).Select(a => a.AllowanceId!.Value).ToList();
                        var allowancesToRemove = employeeEntity.EmployeeAllowances.Where(a => !a.IsDeleted && !currentAllowanceIds.Contains(a.EmployeeAllowanceId)).ToList();

                        foreach (var allowance in allowancesToRemove)
                        {
                            allowance.IsDeleted = true;
                            allowance.DeletedAt = currentTime;
                        }

                        foreach (var item in model.Allowances)
                        {
                            if (!item.AllowanceTypeId.HasValue || !item.FromDate.HasValue) continue;

                            if (item.AllowanceId.HasValue && item.AllowanceId.Value > 0)
                            {
                                var existingAllowance = employeeEntity.EmployeeAllowances.FirstOrDefault(a => a.EmployeeAllowanceId == item.AllowanceId.Value);
                                if (existingAllowance != null)
                                {
                                    existingAllowance.AllowanceTypeId = item.AllowanceTypeId.Value;
                                    existingAllowance.Amount = item.Amount.Value;
                                    existingAllowance.FromDate = item.FromDate.Value;
                                    existingAllowance.ToDate = item.ToDate;
                                    existingAllowance.Notes = item.Notes?.Trim();
                                    existingAllowance.IsActive = model.IsActive;
                                    existingAllowance.UpdatedAt = currentTime;
                                }
                            }
                            else
                            {
                                var newAllowance = new EmployeeAllowance
                                {
                                    EmployeeId = id,
                                    AllowanceTypeId = item.AllowanceTypeId.Value,
                                    Amount = item.Amount.Value,
                                    FromDate = item.FromDate.Value,
                                    ToDate = item.ToDate,
                                    Notes = item.Notes?.Trim(),
                                    IsActive = model.IsActive,
                                    CreatedAt = currentTime,
                                    IsDeleted = false
                                };
                                employeeEntity.EmployeeAllowances.Add(newAllowance);
                            }
                        }
                    }

                    // 13. Deductions
                    if (model.Deductions != null)
                    {
                        var currentDeductionIds = model.Deductions.Where(d => d.DeductionId.HasValue).Select(d => d.DeductionId!.Value).ToList();
                        var deductionsToRemove = employeeEntity.EmployeeDeductions.Where(d => !d.IsDeleted && !currentDeductionIds.Contains(d.EmployeeDeductionId)).ToList();

                        foreach (var deduction in deductionsToRemove)
                        {
                            deduction.IsDeleted = true;
                            deduction.DeletedAt = currentTime;
                        }

                        foreach (var item in model.Deductions)
                        {
                            if (!item.DeductionTypeId.HasValue || !item.FromDate.HasValue) continue;

                            if (item.DeductionId.HasValue && item.DeductionId.Value > 0)
                            {
                                var existingDeduction = employeeEntity.EmployeeDeductions.FirstOrDefault(d => d.EmployeeDeductionId == item.DeductionId.Value);
                                if (existingDeduction != null)
                                {
                                    existingDeduction.DeductionTypeId = item.DeductionTypeId.Value;
                                    existingDeduction.Amount = item.Amount.Value;
                                    existingDeduction.FromDate = item.FromDate.Value;
                                    existingDeduction.ToDate = item.ToDate;
                                    existingDeduction.Notes = item.Notes?.Trim();
                                    existingDeduction.IsActive = model.IsActive;
                                    existingDeduction.UpdatedAt = currentTime;
                                }
                            }
                            else
                            {
                                var newDeduction = new EmployeeDeduction
                                {
                                    EmployeeId = id,
                                    DeductionTypeId = item.DeductionTypeId.Value,
                                    Amount = item.Amount.Value,
                                    FromDate = item.FromDate.Value,
                                    ToDate = item.ToDate,
                                    Notes = item.Notes?.Trim(),
                                    IsActive = model.IsActive,
                                    CreatedAt = currentTime,
                                    IsDeleted = false
                                };
                                employeeEntity.EmployeeDeductions.Add(newDeduction);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    TempData["SuccessMessage"] = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar"
                        ? "تم تحديث بيانات الموظف بنجاح" : "Employee data has been updated successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    var message = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar" ?
                        "حدث خطأ أثناء حفظ البيانات: " : "An error occurred while saving the data: ";

                    ModelState.AddModelError("", message + ex.Message);
                }
            }

            await PopulateLookupsAsync(model.CountryId, model.GovernorateId);
            return View(model);
        }
    }
}