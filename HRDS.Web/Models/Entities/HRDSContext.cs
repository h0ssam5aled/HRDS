using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HRDS.Web.Models.Entities;

public partial class HRDSContext : DbContext
{
    public HRDSContext()
    {
    }

    public HRDSContext(DbContextOptions<HRDSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicFaculty> AcademicFaculties { get; set; }

    public virtual DbSet<AcademicMajor> AcademicMajors { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<AllowanceType> AllowanceTypes { get; set; }

    public virtual DbSet<ApproverType> ApproverTypes { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<AssignmentReason> AssignmentReasons { get; set; }

    public virtual DbSet<AttendanceLog> AttendanceLogs { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }

    public virtual DbSet<BankBranch> BankBranches { get; set; }

    public virtual DbSet<BonusType> BonusTypes { get; set; }

    public virtual DbSet<BusinessMissionExpense> BusinessMissionExpenses { get; set; }

    public virtual DbSet<BusinessMissionRequest> BusinessMissionRequests { get; set; }

    public virtual DbSet<BusinessMissionType> BusinessMissionTypes { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyAsset> CompanyAssets { get; set; }

    public virtual DbSet<CompanyBranch> CompanyBranches { get; set; }

    public virtual DbSet<CostCenter> CostCenters { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CurrencyRate> CurrencyRates { get; set; }

    public virtual DbSet<DeductionType> DeductionTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<EducationGrade> EducationGrades { get; set; }

    public virtual DbSet<EducationQualification> EducationQualifications { get; set; }

    public virtual DbSet<EducationalInstitution> EducationalInstitutions { get; set; }

    public virtual DbSet<EducationalInstitutionType> EducationalInstitutionTypes { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAllowance> EmployeeAllowances { get; set; }

    public virtual DbSet<EmployeeAssetAssignment> EmployeeAssetAssignments { get; set; }

    public virtual DbSet<EmployeeBankAccount> EmployeeBankAccounts { get; set; }

    public virtual DbSet<EmployeeBonuse> EmployeeBonuses { get; set; }

    public virtual DbSet<EmployeeDeduction> EmployeeDeductions { get; set; }

    public virtual DbSet<EmployeeLeaveBalance> EmployeeLeaveBalances { get; set; }

    public virtual DbSet<EmployeeOvertimeRequest> EmployeeOvertimeRequests { get; set; }

    public virtual DbSet<EmployeePenalty> EmployeePenalties { get; set; }

    public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }

    public virtual DbSet<EmployeeQualification> EmployeeQualifications { get; set; }

    public virtual DbSet<EmployeeRemoteWorkRequest> EmployeeRemoteWorkRequests { get; set; }

    public virtual DbSet<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }

    public virtual DbSet<EmployeeSalaryHistory> EmployeeSalaryHistories { get; set; }

    public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }

    public virtual DbSet<EmployeeWorkSchedule> EmployeeWorkSchedules { get; set; }

    public virtual DbSet<EmployeesDatum> EmployeesData { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

    public virtual DbSet<ExpenseType> ExpenseTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Governorate> Governorates { get; set; }

    public virtual DbSet<HolidayCalendar> HolidayCalendars { get; set; }

    public virtual DbSet<JobGroup> JobGroups { get; set; }

    public virtual DbSet<JobLevel> JobLevels { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<LeaveCategory> LeaveCategories { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveRequestApproval> LeaveRequestApprovals { get; set; }

    public virtual DbSet<LeaveRequestAttachment> LeaveRequestAttachments { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<LoanInstallment> LoanInstallments { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<MilitaryStatus> MilitaryStatuses { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<ModelAction> ModelActions { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<OrganizationTree> OrganizationTrees { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PayrollRun> PayrollRuns { get; set; }

    public virtual DbSet<PenaltyType> PenaltyTypes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionRequest> PermissionRequests { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<PositionStatus> PositionStatuses { get; set; }

    public virtual DbSet<ProbationPeriod> ProbationPeriods { get; set; }

    public virtual DbSet<ProbationStatus> ProbationStatuses { get; set; }

    public virtual DbSet<ProcessType> ProcessTypes { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<ResignationReason> ResignationReasons { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<SafetyIncident> SafetyIncidents { get; set; }

    public virtual DbSet<SafetyType> SafetyTypes { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftBreak> ShiftBreaks { get; set; }

    public virtual DbSet<ShiftPattern> ShiftPatterns { get; set; }

    public virtual DbSet<ShiftPatternDetail> ShiftPatternDetails { get; set; }

    public virtual DbSet<ShiftType> ShiftTypes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccess> UserAccesses { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<WorkflowStepsConfig> WorkflowStepsConfigs { get; set; }

    public virtual DbSet<WorkflowTemplate> WorkflowTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicFaculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Academic__306F630E9877D1C7");

            entity.ToTable("AcademicFaculties", "HR");

            entity.HasIndex(e => e.InstitutionId, "IX_AcademicFaculties_InstitutionId");

            entity.Property(e => e.FacultyCode).HasMaxLength(50);
            entity.Property(e => e.FacultyNameAr).HasMaxLength(200);
            entity.Property(e => e.FacultyNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Institution).WithMany(p => p.AcademicFaculties)
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRAcademicFaculties_Institutions");
        });

        modelBuilder.Entity<AcademicMajor>(entity =>
        {
            entity.HasKey(e => e.MajorId).HasName("PK__Academic__D5B8BF91426CF0F9");

            entity.ToTable("AcademicMajors", "HR");

            entity.HasIndex(e => e.FacultyId, "IX_AcademicMajors_FacultyId");

            entity.Property(e => e.MajorCode).HasMaxLength(50);
            entity.Property(e => e.MajorNameAr).HasMaxLength(200);
            entity.Property(e => e.MajorNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Faculty).WithMany(p => p.AcademicMajors)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRAcademicMajors_Faculties");
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A668E22FCA");

            entity.ToTable("Accounts", "FI");

            entity.HasIndex(e => e.AccountTypeId, "IX_Accounts_AccountTypeId");

            entity.HasIndex(e => e.CurrencyId, "IX_Accounts_CurrencyId");

            entity.HasIndex(e => e.ParentAccountId, "IX_Accounts_ParentAccountId");

            entity.Property(e => e.AccountCode).HasMaxLength(50);
            entity.Property(e => e.AccountLevel).HasDefaultValue((byte)1);
            entity.Property(e => e.AccountNameAr).HasMaxLength(200);
            entity.Property(e => e.AccountNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.AccountNature)
                .HasMaxLength(10)
                .HasDefaultValue("Debit");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.HierarchyPath).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLeaf).HasDefaultValue(true);

            entity.HasOne(d => d.AccountType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.AccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FIAccounts_FIAccountTypes");

            entity.HasOne(d => d.Currency).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_FIAccounts_FICurrencies");

            entity.HasOne(d => d.ParentAccount).WithMany(p => p.InverseParentAccount)
                .HasForeignKey(d => d.ParentAccountId)
                .HasConstraintName("FK_FIAccounts_Parent");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId).HasName("PK__AccountT__8F9585AFBE176E57");

            entity.ToTable("AccountTypes", "FI");

            entity.Property(e => e.AccountTypeCode).HasMaxLength(50);
            entity.Property(e => e.AccountTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.AccountTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasKey(e => e.ActionId).HasName("PK_Security_Actions");

            entity.ToTable("Actions", "Security");

            entity.HasIndex(e => e.ActionCode, "UQ_Security_Actions_ActionCode").IsUnique();

            entity.Property(e => e.ActionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActionNameAr).HasMaxLength(200);
            entity.Property(e => e.ActionNameEn).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AllowanceType>(entity =>
        {
            entity.HasKey(e => e.AllowanceTypeId).HasName("PK__Allowanc__F68EBDE9025BA3B7");

            entity.ToTable("AllowanceTypes", "HR");

            entity.Property(e => e.AllowanceTypeCode).HasMaxLength(50);
            entity.Property(e => e.AllowanceTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.AllowanceTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ApproverType>(entity =>
        {
            entity.HasKey(e => e.ApproverTypeId).HasName("PK__Approver__B9DBD889017A75F1");

            entity.ToTable("ApproverTypes", "HR");

            entity.Property(e => e.ApproverTypeCode).HasMaxLength(50);
            entity.Property(e => e.ApproverTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.ApproverTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.AssetTypeId).HasName("PK__AssetTyp__FD33C2C2E869185F");

            entity.ToTable("AssetTypes", "HR");

            entity.Property(e => e.AssetTypeCode).HasMaxLength(50);
            entity.Property(e => e.AssetTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.AssetTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AssignmentReason>(entity =>
        {
            entity.HasKey(e => e.AssignmentReasonId).HasName("PK__Assignme__EA26FF770C2EE8F3");

            entity.ToTable("AssignmentReasons", "HR");

            entity.Property(e => e.AssignmentReasonCode).HasMaxLength(50);
            entity.Property(e => e.AssignmentReasonNameAr).HasMaxLength(200);
            entity.Property(e => e.AssignmentReasonNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AttendanceLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Attendan__5E5486488C6F5FB5");

            entity.ToTable("AttendanceLogs", "HR");

            entity.HasIndex(e => new { e.EmployeeId, e.LogDateTime }, "IX_HRAttendanceLogs_Employee_DateTime");

            entity.Property(e => e.DeviceSerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.AttendanceLogs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRAttendanceLogs_Employee");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PK__Banks__AA08CB13E2605B2E");

            entity.ToTable("Banks", "FI");

            entity.Property(e => e.BankCode).HasMaxLength(50);
            entity.Property(e => e.BankNameAr).HasMaxLength(200);
            entity.Property(e => e.BankNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BankAccountType>(entity =>
        {
            entity.HasKey(e => e.BankAccountTypeId).HasName("PK__BankAcco__AF650FC13E1E163A");

            entity.ToTable("BankAccountTypes", "FI");

            entity.Property(e => e.BankAccountTypeCode).HasMaxLength(50);
            entity.Property(e => e.BankAccountTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.BankAccountTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BankBranch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BankBran__A1682FC55863140B");

            entity.ToTable("BankBranches", "FI");

            entity.HasIndex(e => e.BankId, "IX_BankBranches_BankId");

            entity.Property(e => e.BankBranchAddress).HasMaxLength(300);
            entity.Property(e => e.BankBranchCode).HasMaxLength(50);
            entity.Property(e => e.BankBranchNameAr).HasMaxLength(200);
            entity.Property(e => e.BankBranchNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchPhone).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Bank).WithMany(p => p.BankBranches)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FIBankBranches_FIBanks");
        });

        modelBuilder.Entity<BonusType>(entity =>
        {
            entity.HasKey(e => e.BonusTypeId).HasName("PK__BonusTyp__88343C7D787DDD1A");

            entity.ToTable("BonusTypes", "HR");

            entity.Property(e => e.BonusTypeCode).HasMaxLength(50);
            entity.Property(e => e.BonusTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.BonusTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsTaxable).HasDefaultValue(true);
        });

        modelBuilder.Entity<BusinessMissionExpense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("PK__Business__1445CFD348C42222");

            entity.ToTable("BusinessMissionExpenses", "HR");

            entity.HasIndex(e => e.ExpenseTypeId, "IX_BusinessMissionExpenses_ExpenseTypeId");

            entity.HasIndex(e => e.MissionRequestId, "IX_BusinessMissionExpenses_MissionRequestId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ApprovedAmount)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AttachmentPath).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ReceiptNumber).HasMaxLength(100);

            entity.HasOne(d => d.ExpenseType).WithMany(p => p.BusinessMissionExpenses)
                .HasForeignKey(d => d.ExpenseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessMissionExpenses_ExpenseType");

            entity.HasOne(d => d.MissionRequest).WithMany(p => p.BusinessMissionExpenses)
                .HasForeignKey(d => d.MissionRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRBusinessMissionExpenses_Request");
        });

        modelBuilder.Entity<BusinessMissionRequest>(entity =>
        {
            entity.HasKey(e => e.MissionRequestId).HasName("PK__Business__1B9D507A25EC0E18");

            entity.ToTable("BusinessMissionRequests", "HR");

            entity.HasIndex(e => e.MissionTypeId, "IX_BusinessMissionRequests_MissionTypeId");

            entity.HasIndex(e => e.OverallStatusId, "IX_BusinessMissionRequests_OverallStatusId");

            entity.HasIndex(e => new { e.EmployeeId, e.StartDate }, "IX_HRBusinessMissionRequests_Employee_StartDate").IsDescending(false, true);

            entity.Property(e => e.AttachmentPath).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Destination).HasMaxLength(500);
            entity.Property(e => e.EstimatedAllowance)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Purpose).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithMany(p => p.BusinessMissionRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRBusinessMissionRequests_Employee");

            entity.HasOne(d => d.MissionType).WithMany(p => p.BusinessMissionRequests)
                .HasForeignKey(d => d.MissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRBusinessMissionRequests_Type");

            entity.HasOne(d => d.OverallStatus).WithMany(p => p.BusinessMissionRequests)
                .HasForeignKey(d => d.OverallStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRBusinessMissionRequests_Status");
        });

        modelBuilder.Entity<BusinessMissionType>(entity =>
        {
            entity.HasKey(e => e.MissionTypeId).HasName("PK__Business__328E44EE973131BC");

            entity.ToTable("BusinessMissionTypes", "HR");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MissionTypeCode).HasMaxLength(50);
            entity.Property(e => e.MissionTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.MissionTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK_Config_Cities");

            entity.ToTable("Cities", "Config");

            entity.HasIndex(e => new { e.GovernorateId, e.CityId }, "AK_Cities_GovernorateId_CityId").IsUnique();

            entity.HasIndex(e => e.GovernorateId, "IX_Config_Cities_GovernorateId");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Cities_IsActive_SortOrder");

            entity.HasIndex(e => new { e.GovernorateId, e.CityId }, "UQ_Config_Cities_Governorate_City").IsUnique();

            entity.HasIndex(e => new { e.GovernorateId, e.CityCode }, "UQ_Config_Cities_Governorate_Code").IsUnique();

            entity.Property(e => e.CityCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CityNameAr).HasMaxLength(200);
            entity.Property(e => e.CityNameEn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Governorate).WithMany(p => p.Cities)
                .HasForeignKey(d => d.GovernorateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_Cities_Governorates");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_Core_Companies");

            entity.ToTable("Companies", "Core");

            entity.HasIndex(e => new { e.CountryId, e.GovernorateId }, "IX_Core_Companies_CountryId_GovernorateId");

            entity.HasIndex(e => new { e.GovernorateId, e.CityId }, "IX_Core_Companies_GovernorateId_CityId");

            entity.HasIndex(e => e.CompanyCode, "UQ_Core_Companies_CompanyCode").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.CommercialRegister).HasMaxLength(50);
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CompanyNameAr).HasMaxLength(200);
            entity.Property(e => e.CompanyNameEn).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.TaxNumber).HasMaxLength(50);
            entity.Property(e => e.Website).HasMaxLength(200);

            entity.HasOne(d => d.Governorate).WithMany(p => p.Companies)
                .HasPrincipalKey(p => new { p.CountryId, p.GovernorateId })
                .HasForeignKey(d => new { d.CountryId, d.GovernorateId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_Companies_Governorate");

            entity.HasOne(d => d.City).WithMany(p => p.Companies)
                .HasPrincipalKey(p => new { p.GovernorateId, p.CityId })
                .HasForeignKey(d => new { d.GovernorateId, d.CityId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_Companies_City");
        });

        modelBuilder.Entity<CompanyAsset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__CompanyA__43492352BAEB217E");

            entity.ToTable("CompanyAssets", "HR");

            entity.HasIndex(e => e.AssetTypeId, "IX_CompanyAssets_AssetTypeId");

            entity.HasIndex(e => e.AssetCode, "UQ__CompanyA__2DDE5240917F0B36").IsUnique();

            entity.Property(e => e.AssetCode).HasMaxLength(50);
            entity.Property(e => e.AssetNameAr).HasMaxLength(200);
            entity.Property(e => e.AssetNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.SerialNumber).HasMaxLength(100);

            entity.HasOne(d => d.AssetType).WithMany(p => p.CompanyAssets)
                .HasForeignKey(d => d.AssetTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyAssets_AssetType");
        });

        modelBuilder.Entity<CompanyBranch>(entity =>
        {
            entity.HasKey(e => e.CompanyBranchId).HasName("PK_Core_CompanyBranches");

            entity.ToTable("CompanyBranches", "Core");

            entity.HasIndex(e => new { e.CompanyId, e.CompanyBranchId }, "AK_CompanyBranches_CompanyId_CompanyBranchId").IsUnique();

            entity.HasIndex(e => e.CompanyId, "IX_Core_CompanyBranches_CompanyId");

            entity.HasIndex(e => new { e.CountryId, e.GovernorateId }, "IX_Core_CompanyBranches_CountryId_GovernorateId");

            entity.HasIndex(e => new { e.GovernorateId, e.CityId }, "IX_Core_CompanyBranches_GovernorateId_CityId");

            entity.HasIndex(e => new { e.CompanyId, e.CompanyBranchId }, "UQ_Core_CompanyBranches_Company_Branch").IsUnique();

            entity.HasIndex(e => new { e.CompanyId, e.BranchCode }, "UQ_Core_CompanyBranches_Company_BranchCode").IsUnique();

            entity.HasIndex(e => e.CompanyId, "UX_Core_CompanyBranches_MainBranch")
                .IsUnique()
                .HasFilter("([IsMainBranch]=(1))");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BranchNameAr).HasMaxLength(200);
            entity.Property(e => e.BranchNameEn).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Phone).HasMaxLength(30);

            entity.HasOne(d => d.Company).WithOne(p => p.CompanyBranch)
                .HasForeignKey<CompanyBranch>(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_CompanyBranches_Company");

            entity.HasOne(d => d.Governorate).WithMany(p => p.CompanyBranches)
                .HasPrincipalKey(p => new { p.CountryId, p.GovernorateId })
                .HasForeignKey(d => new { d.CountryId, d.GovernorateId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_CompanyBranches_Governorate");

            entity.HasOne(d => d.City).WithMany(p => p.CompanyBranches)
                .HasPrincipalKey(p => new { p.GovernorateId, p.CityId })
                .HasForeignKey(d => new { d.GovernorateId, d.CityId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Core_CompanyBranches_City");
        });

        modelBuilder.Entity<CostCenter>(entity =>
        {
            entity.HasKey(e => e.CostCenterId).HasName("PK__CostCent__89D876F1E996739B");

            entity.ToTable("CostCenters", "FI");

            entity.HasIndex(e => e.ParentCostCenterId, "IX_CostCenters_ParentCostCenterId");

            entity.Property(e => e.CostCenterCode).HasMaxLength(50);
            entity.Property(e => e.CostCenterLevel).HasDefaultValue((byte)1);
            entity.Property(e => e.CostCenterNameAr).HasMaxLength(200);
            entity.Property(e => e.CostCenterNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HierarchyPath).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsLeaf).HasDefaultValue(true);

            entity.HasOne(d => d.ParentCostCenter).WithMany(p => p.InverseParentCostCenter)
                .HasForeignKey(d => d.ParentCostCenterId)
                .HasConstraintName("FK_FICostCenters_Parent");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK_Config_Countries");

            entity.ToTable("Countries", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Countries_IsActive_SortOrder");

            entity.HasIndex(e => e.CountryCode2, "UQ_Config_Countries_Code2").IsUnique();

            entity.HasIndex(e => e.CountryCode3, "UQ_Config_Countries_Code3").IsUnique();

            entity.Property(e => e.CountryCode2)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode3)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CountryNameAr).HasMaxLength(200);
            entity.Property(e => e.CountryNameEn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PK__Currenci__14470AF01BBD2FB0");

            entity.ToTable("Currencies", "FI");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.CurrencyCode).HasMaxLength(50);
            entity.Property(e => e.CurrencyNameAr).HasMaxLength(200);
            entity.Property(e => e.CurrencyNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Symbol).HasMaxLength(20);
        });

        modelBuilder.Entity<CurrencyRate>(entity =>
        {
            entity.HasKey(e => e.CurrencyRateId).HasName("PK__Currency__A809ECB89EC0F6C3");

            entity.ToTable("CurrencyRates", "FI");

            entity.HasIndex(e => e.BaseCurrencyId, "IX_CurrencyRates_BaseCurrencyId");

            entity.HasIndex(e => e.CurrencyId, "IX_CurrencyRates_CurrencyId");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ExchangeRate).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BaseCurrency).WithMany(p => p.CurrencyRateBaseCurrencies)
                .HasForeignKey(d => d.BaseCurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FICurrencyRates_BaseCurrency");

            entity.HasOne(d => d.Currency).WithMany(p => p.CurrencyRateCurrencies)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FICurrencyRates_FICurrencies");
        });

        modelBuilder.Entity<DeductionType>(entity =>
        {
            entity.HasKey(e => e.DeductionTypeId).HasName("PK__Deductio__AEB1B7AD5CB97E37");

            entity.ToTable("DeductionTypes", "HR");

            entity.Property(e => e.DeductionTypeCode).HasMaxLength(50);
            entity.Property(e => e.DeductionTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.DeductionTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED336F49F1");

            entity.ToTable("Departments", "HR");

            entity.HasIndex(e => e.DepartmentCode, "IX_HRDepartments_Code");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            entity.Property(e => e.DepartmentLevel).HasDefaultValue((byte)1);
            entity.Property(e => e.DepartmentNameAr).HasMaxLength(200);
            entity.Property(e => e.DepartmentNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ParentDepartment).WithMany(p => p.InverseParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId)
                .HasConstraintName("FK_Departments_ParentDepartment");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0FEC61961F");

            entity.ToTable("Documents", "HR");

            entity.HasIndex(e => e.DocumentTypeId, "IX_Documents_DocumentTypeId");

            entity.HasIndex(e => e.EmployeeId, "IX_Documents_EmployeeId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DocumentNumber).HasMaxLength(50);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRDocuments_Type");

            entity.HasOne(d => d.Employee).WithMany(p => p.Documents)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRDocuments_Employee");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.DocumentTypeId).HasName("PK__Document__DBA390E1946981DE");

            entity.ToTable("DocumentTypes", "HR");

            entity.Property(e => e.TypeCode).HasMaxLength(50);
            entity.Property(e => e.TypeNameAr).HasMaxLength(200);
            entity.Property(e => e.TypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EducationGrade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Educatio__54F87A574DEFE2BA");

            entity.ToTable("EducationGrades", "HR");

            entity.Property(e => e.GradeId).ValueGeneratedNever();
            entity.Property(e => e.GradeCode).HasMaxLength(50);
            entity.Property(e => e.GradeNameAr).HasMaxLength(200);
            entity.Property(e => e.GradeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EducationQualification>(entity =>
        {
            entity.HasKey(e => e.QualificationId).HasName("PK__Educatio__C95C12AA4BCDED5F");

            entity.ToTable("EducationQualifications", "HR");

            entity.Property(e => e.QualificationCode).HasMaxLength(50);
            entity.Property(e => e.QualificationNameAr).HasMaxLength(200);
            entity.Property(e => e.QualificationNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EducationalInstitution>(entity =>
        {
            entity.HasKey(e => e.InstitutionId).HasName("PK__Educatio__8DF6B6ADF23BFD8C");

            entity.ToTable("EducationalInstitutions", "HR");

            entity.HasIndex(e => e.InstitutionTypeId, "IX_EducationalInstitutions_InstitutionTypeId");

            entity.Property(e => e.InstitutionCode).HasMaxLength(50);
            entity.Property(e => e.InstitutionNameAr).HasMaxLength(200);
            entity.Property(e => e.InstitutionNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.InstitutionType).WithMany(p => p.EducationalInstitutions)
                .HasForeignKey(d => d.InstitutionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREducationalInstitutions_Types");
        });

        modelBuilder.Entity<EducationalInstitutionType>(entity =>
        {
            entity.HasKey(e => e.InstitutionTypeId).HasName("PK__Educatio__2677D2F171E047BF");

            entity.ToTable("EducationalInstitutionTypes", "HR");

            entity.Property(e => e.InstitutionTypeCode).HasMaxLength(50);
            entity.Property(e => e.InstitutionTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.InstitutionTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId).HasName("PK__Emergenc__E8A61D8E94A9016E");

            entity.ToTable("EmergencyContacts", "HR");

            entity.HasIndex(e => e.EmployeeId, "UX_HREmergencyContacts_Primary")
                .IsUnique()
                .HasFilter("([IsPrimary]=(1) AND [IsDeleted]=(0))");

            entity.Property(e => e.AlternativeMobileNo).HasMaxLength(20);
            entity.Property(e => e.AlternativePhone).HasMaxLength(20);
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MobileNumber).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Relationship).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithOne(p => p.EmergencyContact)
                .HasForeignKey<EmergencyContact>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmergencyContacts_Employees");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F116C89F6B3");

            entity.ToTable("Employees", "HR");

            entity.HasIndex(e => e.EmployeeCode, "UQ__Employee__1F642548D5185F3A").IsUnique();

            entity.HasIndex(e => e.DriverLicenseNumber, "UX_HREmployees_DriverLicenseNumber")
                .IsUnique()
                .HasFilter("([DriverLicenseNumber] IS NOT NULL AND [IsDeleted]=(0))");

            entity.HasIndex(e => e.NationalIdNo, "UX_HREmployees_NationalIdNo")
                .IsUnique()
                .HasFilter("([NationalIdNo] IS NOT NULL AND [IsDeleted]=(0))");

            entity.HasIndex(e => e.PassportNumber, "UX_HREmployees_PassportNumber")
                .IsUnique()
                .HasFilter("([PassportNumber] IS NOT NULL AND [IsDeleted]=(0))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DriverLicenseNumber).HasMaxLength(50);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeOldCode).HasMaxLength(50);
            entity.Property(e => e.FirstNameAr).HasMaxLength(20);
            entity.Property(e => e.FirstNameEn)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastNameAr).HasMaxLength(20);
            entity.Property(e => e.LastNameEn)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleNameAr).HasMaxLength(20);
            entity.Property(e => e.MiddleNameEn)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NationalIdNo).HasMaxLength(50);
            entity.Property(e => e.PassportNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeAllowance>(entity =>
        {
            entity.HasKey(e => e.EmployeeAllowanceId).HasName("PK__Employee__64ADC7EC14ED8DAC");

            entity.ToTable("EmployeeAllowances", "HR");

            entity.HasIndex(e => e.AllowanceTypeId, "IX_EmployeeAllowances_AllowanceTypeId");

            entity.HasIndex(e => new { e.EmployeeId, e.AllowanceTypeId }, "UX_HREmployeeAllowances_Current")
                .IsUnique()
                .HasFilter("([ToDate] IS NULL AND [IsActive]=(1) AND [IsDeleted]=(0))");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.AllowanceType).WithMany(p => p.EmployeeAllowances)
                .HasForeignKey(d => d.AllowanceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeAllowances_Type");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAllowances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeAllowances_Employees");
        });

        modelBuilder.Entity<EmployeeAssetAssignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Employee__32499E773F350935");

            entity.ToTable("EmployeeAssetAssignments", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeAssetAssignments_EmployeeId");

            entity.HasIndex(e => e.AssetId, "UX_HREmployeeAssetAssignments_OpenAsset")
                .IsUnique()
                .HasFilter("([ActualReturnDate] IS NULL AND [IsReturned]=(0))");

            entity.Property(e => e.ConditionOnAssignment).HasMaxLength(300);
            entity.Property(e => e.ConditionOnReturn).HasMaxLength(300);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Asset).WithOne(p => p.EmployeeAssetAssignment)
                .HasForeignKey<EmployeeAssetAssignment>(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetAssignments_Asset");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeAssetAssignments)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetAssignments_Employee");
        });

        modelBuilder.Entity<EmployeeBankAccount>(entity =>
        {
            entity.HasKey(e => e.EmployeeBankId).HasName("PK__Employee__ADD43A214B24717C");

            entity.ToTable("EmployeeBankAccounts", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeBankAccounts_EmployeeId");

            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Iban)
                .HasMaxLength(50)
                .HasColumnName("IBAN");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPrimary).HasDefaultValue(true);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeBankAccounts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeBankAccounts_Employee");
        });

        modelBuilder.Entity<EmployeeBonuse>(entity =>
        {
            entity.HasKey(e => e.BonusId).HasName("PK__Employee__8E5547680A9E3867");

            entity.ToTable("EmployeeBonuses", "HR");

            entity.HasIndex(e => e.BonusTypeId, "IX_EmployeeBonuses_BonusTypeId");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeBonuses_EmployeeId");

            entity.HasIndex(e => e.PayrollRunId, "IX_EmployeeBonuses_PayrollRunId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.BonusType).WithMany(p => p.EmployeeBonuses)
                .HasForeignKey(d => d.BonusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBonuses_BonusType");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeBonuses)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeBonuses_Employee");

            entity.HasOne(d => d.PayrollRun).WithMany(p => p.EmployeeBonuses)
                .HasForeignKey(d => d.PayrollRunId)
                .HasConstraintName("FK_EmployeeBonuses_PayrollRun");
        });

        modelBuilder.Entity<EmployeeDeduction>(entity =>
        {
            entity.HasKey(e => e.EmployeeDeductionId).HasName("PK__Employee__7EE324D4F786A673");

            entity.ToTable("EmployeeDeductions", "HR");

            entity.HasIndex(e => e.DeductionTypeId, "IX_EmployeeDeductions_DeductionTypeId");

            entity.HasIndex(e => new { e.EmployeeId, e.DeductionTypeId }, "UX_HREmployeeDeductions_Current")
                .IsUnique()
                .HasFilter("([ToDate] IS NULL AND [IsActive]=(1) AND [IsDeleted]=(0))");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.DeductionType).WithMany(p => p.EmployeeDeductions)
                .HasForeignKey(d => d.DeductionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeDeductions_Type");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDeductions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeDeductions_Employees");
        });

        modelBuilder.Entity<EmployeeLeaveBalance>(entity =>
        {
            entity.HasKey(e => e.BalanceId).HasName("PK__Employee__A760D5BE8C2BF5F1");

            entity.ToTable("EmployeeLeaveBalances", "HR");

            entity.HasIndex(e => e.LeaveTypeId, "IX_EmployeeLeaveBalances_LeaveTypeId");

            entity.HasIndex(e => new { e.EmployeeId, e.LeaveTypeId, e.Year }, "UX_HREmployeeLeaveBalances_Employee_LeaveType_Year")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.CarriedForwardDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.EntitledDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PendingDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RemainingBalance).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TakenDays).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeLeaveBalances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeLeaveBalances_Employee");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.EmployeeLeaveBalances)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeLeaveBalances_Type");
        });

        modelBuilder.Entity<EmployeeOvertimeRequest>(entity =>
        {
            entity.HasKey(e => e.OvertimeRequestId).HasName("PK__Employee__F97D0DCA4523EF7C");

            entity.ToTable("EmployeeOvertimeRequests", "HR");

            entity.HasIndex(e => e.OverallStatusId, "IX_EmployeeOvertimeRequests_OverallStatusId");

            entity.HasIndex(e => new { e.EmployeeId, e.OvertimeDate }, "IX_HREmployeeOvertimeRequests_Employee_Date").IsDescending(false, true);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.TotalHours).HasColumnType("decimal(4, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeOvertimeRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeOvertime_Employee");

            entity.HasOne(d => d.OverallStatus).WithMany(p => p.EmployeeOvertimeRequests)
                .HasForeignKey(d => d.OverallStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeOvertime_Status");
        });

        modelBuilder.Entity<EmployeePenalty>(entity =>
        {
            entity.HasKey(e => e.PenaltyId).HasName("PK__Employee__567E06C7D6742B9C");

            entity.ToTable("EmployeePenalties", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeePenalties_EmployeeId");

            entity.HasIndex(e => e.PayrollRunId, "IX_EmployeePenalties_PayrollRunId");

            entity.HasIndex(e => e.PenaltyTypeId, "IX_EmployeePenalties_PenaltyTypeId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DeductionAmount)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.DeductionDays)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Reason).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePenalties)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeePenalties_Employee");

            entity.HasOne(d => d.PayrollRun).WithMany(p => p.EmployeePenalties)
                .HasForeignKey(d => d.PayrollRunId)
                .HasConstraintName("FK_EmployeePenalties_PayrollRun");

            entity.HasOne(d => d.PenaltyType).WithMany(p => p.EmployeePenalties)
                .HasForeignKey(d => d.PenaltyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeePenalties_PenaltyType");
        });

        modelBuilder.Entity<EmployeePosition>(entity =>
        {
            entity.HasKey(e => e.EmployeePositionId).HasName("PK__Employee__6FDE90605DAF4335");

            entity.ToTable("EmployeePositions", "HR");

            entity.HasIndex(e => e.AssignmentReasonId, "IX_EmployeePositions_AssignmentReasonId");

            entity.HasIndex(e => e.PositionId, "IX_EmployeePositions_PositionId");

            entity.HasIndex(e => new { e.EmployeeId, e.FromDate }, "IX_HREmployeePositions_Employee_FromDate").IsDescending(false, true);

            entity.HasIndex(e => e.EmployeeId, "UX_HREmployeePositions_Primary")
                .IsUnique()
                .HasFilter("([PrimaryPosition]=(1) AND [IsActive]=(1) AND [IsDeleted]=(0) AND [ToDate] IS NULL)");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PrimaryPosition).HasDefaultValue(true);

            entity.HasOne(d => d.AssignmentReason).WithMany(p => p.EmployeePositions)
                .HasForeignKey(d => d.AssignmentReasonId)
                .HasConstraintName("FK_HREmployeePositions_Reason");

            entity.HasOne(d => d.Employee).WithOne(p => p.EmployeePosition)
                .HasForeignKey<EmployeePosition>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeePositions_Employee");

            entity.HasOne(d => d.Position).WithMany(p => p.EmployeePositions)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeePositions_Position");
        });

        modelBuilder.Entity<EmployeeQualification>(entity =>
        {
            entity.HasKey(e => e.EmployeeQualificationId).HasName("PK__Employee__E618F3DC0CCA4A30");

            entity.ToTable("EmployeeQualifications", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeQualifications_EmployeeId");

            entity.HasIndex(e => e.FacultyId, "IX_EmployeeQualifications_FacultyId");

            entity.HasIndex(e => e.GradeOrGpa, "IX_EmployeeQualifications_GradeOrGPA");

            entity.HasIndex(e => e.InstitutionId, "IX_EmployeeQualifications_InstitutionId");

            entity.HasIndex(e => e.MajorId, "IX_EmployeeQualifications_MajorId");

            entity.HasIndex(e => e.QualificationId, "IX_EmployeeQualifications_QualificationId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.GradeOrGpa).HasColumnName("GradeOrGPA");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeQualifications_Employee");

            entity.HasOne(d => d.Faculty).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK_EmployeeQualifications_Faculty");

            entity.HasOne(d => d.GradeOrGpaNavigation).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.GradeOrGpa)
                .HasConstraintName("FK_EmployeeQualifications_Grade");

            entity.HasOne(d => d.Institution).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.InstitutionId)
                .HasConstraintName("FK_EmployeeQualifications_Institution");

            entity.HasOne(d => d.Major).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK_EmployeeQualifications_Major");

            entity.HasOne(d => d.Qualification).WithMany(p => p.EmployeeQualifications)
                .HasForeignKey(d => d.QualificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeQualifications_Qualification");
        });

        modelBuilder.Entity<EmployeeRemoteWorkRequest>(entity =>
        {
            entity.HasKey(e => e.RemoteWorkRequestId).HasName("PK__Employee__2FD90AC4D29E4026");

            entity.ToTable("EmployeeRemoteWorkRequests", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeRemoteWorkRequests_EmployeeId");

            entity.HasIndex(e => e.OverallStatusId, "IX_EmployeeRemoteWorkRequests_OverallStatusId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.TotalDays).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRemoteWorkRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RemoteWork_Employee");

            entity.HasOne(d => d.OverallStatus).WithMany(p => p.EmployeeRemoteWorkRequests)
                .HasForeignKey(d => d.OverallStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RemoteWork_Status");
        });

        modelBuilder.Entity<EmployeeSalaryDetail>(entity =>
        {
            entity.HasKey(e => e.SalaryDetailId).HasName("PK__Employee__EE7B1F8483F4474C");

            entity.ToTable("EmployeeSalaryDetails", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeSalaryDetails_EmployeeId");

            entity.HasIndex(e => e.PayrollRunId, "IX_HREmployeeSalaryDetails_PayrollRun");

            entity.HasIndex(e => new { e.PayrollRunId, e.EmployeeId }, "UX_HREmployeeSalaryDetails_Run_Employee")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.AbsenceDeduction).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.AllowancesAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DelayDeduction).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LoansDeduction).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.NetSalary).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.OvertimeAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.SocialInsuranceEmployee).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeSalaryDetails)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeSalaryDetails_Employee");

            entity.HasOne(d => d.PayrollRun).WithMany(p => p.EmployeeSalaryDetails)
                .HasForeignKey(d => d.PayrollRunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeSalaryDetails_Run");
        });

        modelBuilder.Entity<EmployeeSalaryHistory>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__Employee__4BE204579A1860D7");

            entity.ToTable("EmployeeSalaryHistory", "HR");

            entity.HasIndex(e => new { e.EmployeeId, e.FromDate }, "IX_HREmployeeSalaryHistory_Employee_FromDate").IsDescending(false, true);

            entity.HasIndex(e => e.EmployeeId, "UX_HREmployeeSalaryHistory_Current")
                .IsUnique()
                .HasFilter("([ToDate] IS NULL AND [IsActive]=(1) AND [IsDeleted]=(0))");

            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NetSalary).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithOne(p => p.EmployeeSalaryHistory)
                .HasForeignKey<EmployeeSalaryHistory>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeSalaryHistory_Employee");
        });

        modelBuilder.Entity<EmployeeStatus>(entity =>
        {
            entity.HasKey(e => e.EmployeeStatusId).HasName("PK__Employee__3609932C02D4B258");

            entity.ToTable("EmployeeStatus", "HR");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.EmployeeStatusCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeStatusNameAr).HasMaxLength(200);
            entity.Property(e => e.EmployeeStatusNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeWorkSchedule>(entity =>
        {
            entity.HasKey(e => e.EmployeeWorkScheduleId).HasName("PK__Employee__FF1534F4D16910D8");

            entity.ToTable("EmployeeWorkSchedule", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_EmployeeWorkSchedule_EmployeeId");

            entity.HasIndex(e => e.PatternId, "IX_EmployeeWorkSchedule_PatternId");

            entity.HasIndex(e => e.ShiftId, "IX_EmployeeWorkSchedule_ShiftId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Priority).HasDefaultValue((byte)1);
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.ScheduleType).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeWorkSchedules)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeeWorkSchedule_Employee");

            entity.HasOne(d => d.Pattern).WithMany(p => p.EmployeeWorkSchedules)
                .HasForeignKey(d => d.PatternId)
                .HasConstraintName("FK_HREmployeeWorkSchedule_Pattern");

            entity.HasOne(d => d.Shift).WithMany(p => p.EmployeeWorkSchedules)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_HREmployeeWorkSchedule_Shift");
        });

        modelBuilder.Entity<EmployeesDatum>(entity =>
        {
            entity.HasKey(e => e.HremployeeDataId).HasName("PK__Employee__B13F78FFA90109F8");

            entity.ToTable("EmployeesData", "HR");

            entity.HasIndex(e => e.EmployeeId, "UX_HREmployeesData_Employee")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.HremployeeDataId).HasColumnName("HREmployeeDataId");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmployeeAddress).HasMaxLength(500);
            entity.Property(e => e.FirstMobileNo).HasMaxLength(20);
            entity.Property(e => e.FirstPhoneNo).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SecondMobileNo).HasMaxLength(20);
            entity.Property(e => e.SecondPhoneNo).HasMaxLength(20);

            entity.HasOne(d => d.Employee).WithOne(p => p.EmployeesDatum)
                .HasForeignKey<EmployeesDatum>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmployeesData_Employees");
        });

        modelBuilder.Entity<EmploymentHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Employme__4D7B4ABD73122633");

            entity.ToTable("EmploymentHistory", "HR");

            entity.HasIndex(e => e.DepartmentId, "IX_EmploymentHistory_DepartmentId");

            entity.HasIndex(e => e.DirectManagerId, "IX_EmploymentHistory_DirectManagerId");

            entity.HasIndex(e => e.EmployeeStatusId, "IX_EmploymentHistory_EmployeeStatusId");

            entity.HasIndex(e => e.EmploymentTypeId, "IX_EmploymentHistory_EmploymentTypeId");

            entity.HasIndex(e => e.JobLevelId, "IX_EmploymentHistory_JobLevelId");

            entity.HasIndex(e => e.JobTitleId, "IX_EmploymentHistory_JobTitleId");

            entity.HasIndex(e => new { e.EmployeeId, e.HireDate }, "IX_HREmploymentHistory_Employee_HireDate").IsDescending(false, true);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ResonOfLeaving).HasMaxLength(500);

            entity.HasOne(d => d.Department).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmploymentHistory_Department");

            entity.HasOne(d => d.DirectManager).WithMany(p => p.EmploymentHistoryDirectManagers)
                .HasForeignKey(d => d.DirectManagerId)
                .HasConstraintName("FK_HREmploymentHistory_Manager");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmploymentHistoryEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmploymentHistory_Employees");

            entity.HasOne(d => d.EmployeeStatus).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.EmployeeStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmploymentHistory_Status");

            entity.HasOne(d => d.EmploymentType).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.EmploymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmploymentHistory_EmploymentType");

            entity.HasOne(d => d.JobLevel).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.JobLevelId)
                .HasConstraintName("FK_HREmploymentHistory_JobLevel");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.EmploymentHistories)
                .HasForeignKey(d => d.JobTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HREmploymentHistory_JobTitle");
        });

        modelBuilder.Entity<EmploymentType>(entity =>
        {
            entity.HasKey(e => e.EmploymentTypeId).HasName("PK__Employme__01754F311B33E2A7");

            entity.ToTable("EmploymentType", "HR");

            entity.Property(e => e.DefaultWorkingHours).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.EmploymentTypeCode).HasMaxLength(50);
            entity.Property(e => e.EmploymentTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.EmploymentTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsLeaveEligible).HasDefaultValue(true);
            entity.Property(e => e.IsOvertimeAllowed).HasDefaultValue(true);
        });

        modelBuilder.Entity<ExpenseType>(entity =>
        {
            entity.HasKey(e => e.ExpenseTypeId).HasName("PK__ExpenseT__E082A08F76B4D8CE");

            entity.ToTable("ExpenseTypes", "HR");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ExpenseTypeCode).HasMaxLength(50);
            entity.Property(e => e.ExpenseTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.ExpenseTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxLimit).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK_Config_Genders");

            entity.ToTable("Genders", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Genders_IsActive_SortOrder");

            entity.HasIndex(e => e.GenderCode, "UQ_Config_Genders_Code").IsUnique();

            entity.Property(e => e.GenderCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.GenderNameAr).HasMaxLength(200);
            entity.Property(e => e.GenderNameEn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Governorate>(entity =>
        {
            entity.HasKey(e => e.GovernorateId).HasName("PK_Config_Governorates");

            entity.ToTable("Governorates", "Config");

            entity.HasIndex(e => new { e.CountryId, e.GovernorateId }, "AK_Governorates_CountryId_GovernorateId").IsUnique();

            entity.HasIndex(e => e.CountryId, "IX_Config_Governorates_CountryId");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Governorates_IsActive_SortOrder");

            entity.HasIndex(e => new { e.CountryId, e.GovernorateCode }, "UQ_Config_Governorates_Country_Code").IsUnique();

            entity.HasIndex(e => new { e.CountryId, e.GovernorateId }, "UQ_Config_Governorates_Country_Governorate").IsUnique();

            entity.Property(e => e.GovernorateCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.GovernorateNameAr).HasMaxLength(200);
            entity.Property(e => e.GovernorateNameEn).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Country).WithMany(p => p.Governorates)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Config_Governorates_Countries");
        });

        modelBuilder.Entity<HolidayCalendar>(entity =>
        {
            entity.HasKey(e => e.HolidayId).HasName("PK__HolidayC__2D35D57A9584D3CE");

            entity.ToTable("HolidayCalendar", "HR");

            entity.Property(e => e.HolidayNameAr).HasMaxLength(200);
            entity.Property(e => e.HolidayNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TotalDays).HasDefaultValue((short)1);
        });

        modelBuilder.Entity<JobGroup>(entity =>
        {
            entity.HasKey(e => e.JobGroupId).HasName("PK__JobGroup__B2D5761991FAAC09");

            entity.ToTable("JobGroups", "HR");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobGroupCode).HasMaxLength(50);
            entity.Property(e => e.JobGroupNameAr).HasMaxLength(200);
            entity.Property(e => e.JobGroupNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobLevel>(entity =>
        {
            entity.HasKey(e => e.JobLevelId).HasName("PK__JobLevel__7594C8ACC5AB3FA1");

            entity.ToTable("JobLevels", "HR");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobLevelCode).HasMaxLength(50);
            entity.Property(e => e.JobLevelNameAr).HasMaxLength(200);
            entity.Property(e => e.JobLevelNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId).HasName("PK__JobTitle__35382FE9F0F79688");

            entity.ToTable("JobTitles", "HR");

            entity.HasIndex(e => e.JobTitleCode, "IX_HRJobTitles_Code");

            entity.HasIndex(e => e.JobGroupId, "IX_JobTitles_JobGroupId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JobTitleCode).HasMaxLength(50);
            entity.Property(e => e.JobTitleNameAr).HasMaxLength(200);
            entity.Property(e => e.JobTitleNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.JobGroup).WithMany(p => p.JobTitles)
                .HasForeignKey(d => d.JobGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRJobTitles_HRJobGroups");
        });

        modelBuilder.Entity<LeaveCategory>(entity =>
        {
            entity.HasKey(e => e.LeaveCategoryId).HasName("PK_HRLeaveCategories");

            entity.ToTable("LeaveCategories", "HR");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LeaveCategoryCode).HasMaxLength(50);
            entity.Property(e => e.LeaveCategoryNameAr).HasMaxLength(200);
            entity.Property(e => e.LeaveCategoryNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__609421EEE23522EF");

            entity.ToTable("LeaveRequests", "HR");

            entity.HasIndex(e => new { e.EmployeeId, e.StartDate }, "IX_HRLeaveRequests_Employee_StartDate").IsDescending(false, true);

            entity.HasIndex(e => e.LeaveTypeId, "IX_LeaveRequests_LeaveTypeId");

            entity.HasIndex(e => e.OverallStatusId, "IX_LeaveRequests_OverallStatusId");

            entity.HasIndex(e => e.SubstituteEmployeeId, "IX_LeaveRequests_SubstituteEmployeeId");

            entity.Property(e => e.AttachmentPath).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.TotalDays).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveRequestEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequests_Employee");

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequests_Type");

            entity.HasOne(d => d.OverallStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.OverallStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequests_Status");

            entity.HasOne(d => d.SubstituteEmployee).WithMany(p => p.LeaveRequestSubstituteEmployees)
                .HasForeignKey(d => d.SubstituteEmployeeId)
                .HasConstraintName("FK_HRLeaveRequests_Substitute");
        });

        modelBuilder.Entity<LeaveRequestApproval>(entity =>
        {
            entity.HasKey(e => e.ApprovalId).HasName("PK__LeaveReq__328477F485B0F26C");

            entity.ToTable("LeaveRequestApprovals", "HR");

            entity.HasIndex(e => e.ApproverEmployeeId, "IX_LeaveRequestApprovals_ApproverEmployeeId");

            entity.HasIndex(e => e.StatusId, "IX_LeaveRequestApprovals_StatusId");

            entity.HasIndex(e => new { e.LeaveRequestId, e.StepOrder }, "UX_HRLeaveRequestApprovals_Request_Step")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ApproverEmployee).WithMany(p => p.LeaveRequestApprovals)
                .HasForeignKey(d => d.ApproverEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequestApprovals_Approver");

            entity.HasOne(d => d.LeaveRequest).WithMany(p => p.LeaveRequestApprovals)
                .HasForeignKey(d => d.LeaveRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequestApprovals_Request");

            entity.HasOne(d => d.Status).WithMany(p => p.LeaveRequestApprovals)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequestApprovals_Status");
        });

        modelBuilder.Entity<LeaveRequestAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__LeaveReq__442C64BE9AC14E49");

            entity.ToTable("LeaveRequestAttachments", "HR");

            entity.HasIndex(e => e.LeaveRequestId, "IX_LeaveRequestAttachments_LeaveRequestId");

            entity.Property(e => e.FileName).HasMaxLength(250);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.FileType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(300);
            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.LeaveRequest).WithMany(p => p.LeaveRequestAttachments)
                .HasForeignKey(d => d.LeaveRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLeaveRequestAttachments_Request");
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK__LeaveTyp__43BE8F14FB480324");

            entity.ToTable("LeaveType", "HR");

            entity.HasIndex(e => e.LeaveCategoryId, "IX_LeaveType_LeaveCategoryId");

            entity.Property(e => e.AllowFutureRequest).HasDefaultValue(true);
            entity.Property(e => e.CarryForwardLimit)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.ColorCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EncashmentLimit).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ExpireAtYearEnd).HasDefaultValue(true);
            entity.Property(e => e.GenderRestriction).HasDefaultValue((byte)0);
            entity.Property(e => e.IconName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsPaid).HasDefaultValue(true);
            entity.Property(e => e.LeaveCode).HasMaxLength(50);
            entity.Property(e => e.LeaveNameAr).HasMaxLength(200);
            entity.Property(e => e.LeaveNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MaxDaysPerRequest).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.MaxDaysPerYear).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.MaximumConsecutiveDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MinimumDaysPerRequest).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MinimumServiceMonths).HasDefaultValue((short)0);
            entity.Property(e => e.RequiresApproval).HasDefaultValue(true);
            entity.Property(e => e.RequiresBalance).HasDefaultValue(true);
            entity.Property(e => e.RequiresWorkflow).HasDefaultValue(true);

            entity.HasOne(d => d.LeaveCategory).WithMany(p => p.LeaveTypes)
                .HasForeignKey(d => d.LeaveCategoryId)
                .HasConstraintName("FK_HRLeaveType_Category");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD457484F9401");

            entity.ToTable("Loans", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_Loans_EmployeeId");

            entity.HasIndex(e => e.StatusId, "IX_Loans_StatusId");

            entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.MonthlyInstallment).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.Employee).WithMany(p => p.Loans)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLoans_Employee");

            entity.HasOne(d => d.Status).WithMany(p => p.Loans)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLoans_Status");
        });

        modelBuilder.Entity<LoanInstallment>(entity =>
        {
            entity.HasKey(e => e.InstallmentId).HasName("PK__LoanInst__42B42D8272F11AAB");

            entity.ToTable("LoanInstallments", "HR");

            entity.HasIndex(e => e.PayrollRunId, "IX_LoanInstallments_PayrollRunId");

            entity.HasIndex(e => new { e.LoanId, e.InstallmentNumber }, "UX_HRLoanInstallments_Loan_Number").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Notes).HasMaxLength(300);

            entity.HasOne(d => d.Loan).WithMany(p => p.LoanInstallments)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRLoanInstallments_Loan");

            entity.HasOne(d => d.PayrollRun).WithMany(p => p.LoanInstallments)
                .HasForeignKey(d => d.PayrollRunId)
                .HasConstraintName("FK_HRLoanInstallments_PayrollRun");
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.HasKey(e => e.MaritalStatusId).HasName("PK_Config_MaritalStatuses");

            entity.ToTable("MaritalStatuses", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_MaritalStatuses_IsActive_SortOrder");

            entity.HasIndex(e => e.MaritalStatusCode, "UQ_Config_MaritalStatuses_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaritalStatusCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaritalStatusNameAr).HasMaxLength(200);
            entity.Property(e => e.MaritalStatusNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<MilitaryStatus>(entity =>
        {
            entity.HasKey(e => e.MilitaryStatusId).HasName("PK_Config_MilitaryStatuses");

            entity.ToTable("MilitaryStatuses", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_MilitaryStatuses_IsActive_SortOrder");

            entity.HasIndex(e => e.MilitaryStatusCode, "UQ_Config_MilitaryStatuses_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MilitaryStatusCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MilitaryStatusNameAr).HasMaxLength(200);
            entity.Property(e => e.MilitaryStatusNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK_Security_Models");

            entity.ToTable("Models", "Security");

            entity.HasIndex(e => e.ModuleId, "IX_Security_Models_ModuleId");

            entity.HasIndex(e => new { e.ModuleId, e.ModelCode }, "UQ_Security_Models_Module_ModelCode").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModelCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModelNameAr).HasMaxLength(200);
            entity.Property(e => e.ModelNameEn).HasMaxLength(200);

            entity.HasOne(d => d.Module).WithMany(p => p.Models)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_Models_Modules");
        });

        modelBuilder.Entity<ModelAction>(entity =>
        {
            entity.ToTable("ModelActions", "Security");

            entity.HasIndex(e => e.ActionId, "IX_ModelActions_ActionId");

            entity.HasIndex(e => new { e.ModelId, e.ActionId }, "UQ_ModelActions_Model_Action").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Action).WithMany(p => p.ModelActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModelActions_Actions");

            entity.HasOne(d => d.Model).WithMany(p => p.ModelActions)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ModelActions_Models");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK_Security_Modules");

            entity.ToTable("Modules", "Security");

            entity.HasIndex(e => e.ModuleCode, "UQ_Security_Modules_ModuleCode").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModuleCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModuleNameAr).HasMaxLength(200);
            entity.Property(e => e.ModuleNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.NationalityId).HasName("PK_Config_Nationalities");

            entity.ToTable("Nationalities", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Nationalities_IsActive_SortOrder");

            entity.HasIndex(e => e.NationalityCode, "UQ_Config_Nationalities_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NationalityCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NationalityNameAr).HasMaxLength(200);
            entity.Property(e => e.NationalityNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<OrganizationTree>(entity =>
        {
            entity.HasKey(e => new { e.AncestorPositionId, e.DescendantPositionId }).HasName("PK__Organiza__930E755F31B1B8A3");

            entity.ToTable("OrganizationTree", "HR");

            entity.HasIndex(e => e.DescendantPositionId, "IX_OrganizationTree_DescendantPositionId");

            entity.HasOne(d => d.AncestorPosition).WithMany(p => p.OrganizationTreeAncestorPositions)
                .HasForeignKey(d => d.AncestorPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrgTree_Ancestor");

            entity.HasOne(d => d.DescendantPosition).WithMany(p => p.OrganizationTreeDescendantPositions)
                .HasForeignKey(d => d.DescendantPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrgTree_Descendant");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1D391DA101B");

            entity.ToTable("PaymentMethods", "HR");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PaymentMethodCode).HasMaxLength(50);
            entity.Property(e => e.PaymentMethodNameAr).HasMaxLength(200);
            entity.Property(e => e.PaymentMethodNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PayrollRun>(entity =>
        {
            entity.HasKey(e => e.PayrollRunId).HasName("PK__PayrollR__8B3CCD4DFFB9A05F");

            entity.ToTable("PayrollRuns", "HR");

            entity.HasIndex(e => new { e.CompanyId, e.Year, e.Month }, "UX_HRPayrollRuns_Company_Year_Month")
                .IsUnique()
                .HasFilter("([CompanyId] IS NOT NULL AND [IsDeleted]=(0))");

            entity.HasIndex(e => new { e.Year, e.Month }, "UX_HRPayrollRuns_Global_Year_Month")
                .IsUnique()
                .HasFilter("([CompanyId] IS NULL AND [IsDeleted]=(0))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.NetSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAllowances).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBasicSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalDeductions).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PenaltyType>(entity =>
        {
            entity.HasKey(e => e.PenaltyTypeId).HasName("PK__PenaltyT__E29F64B120A857BC");

            entity.ToTable("PenaltyTypes", "HR");

            entity.Property(e => e.DefaultDeductionDays).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PenaltyTypeCode).HasMaxLength(50);
            entity.Property(e => e.PenaltyTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.PenaltyTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK_Security_Permissions");

            entity.ToTable("Permissions", "Security");

            entity.HasIndex(e => e.ActionId, "IX_Security_Permissions_ActionId");

            entity.HasIndex(e => e.ModelId, "IX_Security_Permissions_ModelId");

            entity.HasIndex(e => new { e.ModelId, e.ActionId }, "UQ_Security_Permissions_Model_Action").IsUnique();

            entity.HasIndex(e => e.PermissionCode, "UQ_Security_Permissions_PermissionCode").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PermissionCode)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PermissionNameAr).HasMaxLength(200);
            entity.Property(e => e.PermissionNameEn).HasMaxLength(200);

            entity.HasOne(d => d.Action).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_Permissions_Actions");

            entity.HasOne(d => d.Model).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_Permissions_Models");
        });

        modelBuilder.Entity<PermissionRequest>(entity =>
        {
            entity.HasKey(e => e.PermissionRequestId).HasName("PK__Permissi__F0AC4844DA7155BE");

            entity.ToTable("PermissionRequests", "HR");

            entity.HasIndex(e => new { e.EmployeeId, e.PermissionDate }, "IX_HRPermissionRequests_Employee_Date").IsDescending(false, true);

            entity.HasIndex(e => e.OverallStatusId, "IX_PermissionRequests_OverallStatusId");

            entity.HasIndex(e => e.PermissionTypeId, "IX_PermissionRequests_PermissionTypeId");

            entity.Property(e => e.Reason).HasMaxLength(300);

            entity.HasOne(d => d.Employee).WithMany(p => p.PermissionRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRPermissionRequests_Employee");

            entity.HasOne(d => d.OverallStatus).WithMany(p => p.PermissionRequests)
                .HasForeignKey(d => d.OverallStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRPermissionRequests_Status");

            entity.HasOne(d => d.PermissionType).WithMany(p => p.PermissionRequests)
                .HasForeignKey(d => d.PermissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRPermissionRequests_Type");
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.HasKey(e => e.PermissionTypeId).HasName("PK__Permissi__53B420CF093E4DD4");

            entity.ToTable("PermissionTypes", "HR");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxHoursPerMonth).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.PermissionTypeCode).HasMaxLength(50);
            entity.Property(e => e.PermissionTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.PermissionTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A79612B984C");

            entity.ToTable("Positions", "HR");

            entity.HasIndex(e => e.PositionCode, "IX_HRPositions_Code");

            entity.HasIndex(e => e.JobLevelId, "IX_Positions_JobLevelId");

            entity.HasIndex(e => e.JobTitleId, "IX_Positions_JobTitleId");

            entity.HasIndex(e => e.ParentPositionId, "IX_Positions_ParentPositionId");

            entity.HasIndex(e => e.PositionStatusId, "IX_Positions_PositionStatusId");

            entity.HasIndex(e => e.ReportsToPositionId, "IX_Positions_ReportsToPositionId");

            entity.HasIndex(e => e.UnitId, "IX_Positions_UnitId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HeadCount).HasDefaultValue((short)1);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PositionCode).HasMaxLength(50);
            entity.Property(e => e.PositionNameAr).HasMaxLength(200);
            entity.Property(e => e.PositionNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Remarks).HasMaxLength(500);

            entity.HasOne(d => d.JobLevel).WithMany(p => p.Positions)
                .HasForeignKey(d => d.JobLevelId)
                .HasConstraintName("FK_HRPositions_HRJobLevels");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Positions)
                .HasForeignKey(d => d.JobTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRPositions_HRJobTitles");

            entity.HasOne(d => d.ParentPosition).WithMany(p => p.InverseParentPosition)
                .HasForeignKey(d => d.ParentPositionId)
                .HasConstraintName("FK_HRPositions_Parent");

            entity.HasOne(d => d.PositionStatus).WithMany(p => p.Positions)
                .HasForeignKey(d => d.PositionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRPositions_HRPositionStatuses");

            entity.HasOne(d => d.ReportsToPosition).WithMany(p => p.InverseReportsToPosition)
                .HasForeignKey(d => d.ReportsToPositionId)
                .HasConstraintName("FK_HRPositions_ReportsTo");

            entity.HasOne(d => d.Unit).WithMany(p => p.Positions)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_HRPositions_HRUnits");
        });

        modelBuilder.Entity<PositionStatus>(entity =>
        {
            entity.HasKey(e => e.PositionStatusId).HasName("PK__Position__0E69A5617A05E155");

            entity.ToTable("PositionStatuses", "HR");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PositionStatusCode).HasMaxLength(50);
            entity.Property(e => e.PositionStatusNameAr).HasMaxLength(200);
            entity.Property(e => e.PositionStatusNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProbationPeriod>(entity =>
        {
            entity.HasKey(e => e.ProbationId).HasName("PK__Probatio__96EF4E58A0D2A444");

            entity.ToTable("ProbationPeriod", "HR");

            entity.HasIndex(e => e.EmployeeId, "UX_HRProbationPeriod_Current")
                .IsUnique()
                .HasFilter("([IsActive]=(1) AND [IsDeleted]=(0) AND [IsConfirmed]=(0))");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Employee).WithOne(p => p.ProbationPeriod)
                .HasForeignKey<ProbationPeriod>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRProbationPeriod_Employee");
        });

        modelBuilder.Entity<ProbationStatus>(entity =>
        {
            entity.HasKey(e => e.ProbationStatusId).HasName("PK__Probatio__1DCB601DB2B4C638");

            entity.ToTable("ProbationStatuses", "HR");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.StatusCode).HasMaxLength(50);
            entity.Property(e => e.StatusNameAr).HasMaxLength(200);
            entity.Property(e => e.StatusNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProcessType>(entity =>
        {
            entity.HasKey(e => e.ProcessTypeId).HasName("PK__ProcessT__E0D195E481E20F85");

            entity.ToTable("ProcessTypes", "HR");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ProcessCode).HasMaxLength(50);
            entity.Property(e => e.ProcessNameAr).HasMaxLength(200);
            entity.Property(e => e.ProcessNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.HasKey(e => e.ReligionId).HasName("PK_Config_Religions");

            entity.ToTable("Religions", "Config");

            entity.HasIndex(e => new { e.IsActive, e.SortOrder }, "IX_Config_Religions_IsActive_SortOrder");

            entity.HasIndex(e => e.ReligionCode, "UQ_Config_Religions_Code").IsUnique();

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ReligionCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ReligionNameAr).HasMaxLength(200);
            entity.Property(e => e.ReligionNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__RequestS__C8EE20630667610E");

            entity.ToTable("RequestStatuses", "HR");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.BadgeClass)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StatusCode).HasMaxLength(50);
            entity.Property(e => e.StatusNameAr).HasMaxLength(200);
            entity.Property(e => e.StatusNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ResignationReason>(entity =>
        {
            entity.ToTable("ResignationReasons", "HR");

            entity.HasIndex(e => e.ResignationReasonCode, "UX_ResignationReasons_Code")
                .IsUnique()
                .HasFilter("([IsDeleted]=(0))");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ResignationReasonCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ResignationReasonNameAr).HasMaxLength(100);
            entity.Property(e => e.ResignationReasonNameEn).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Security_Roles");

            entity.ToTable("Roles", "Security");

            entity.HasIndex(e => e.RoleCode, "UQ_Security_Roles_RoleCode").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.RoleCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoleNameAr).HasMaxLength(200);
            entity.Property(e => e.RoleNameEn).HasMaxLength(200);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId }).HasName("PK_Security_RolePermissions");

            entity.ToTable("RolePermissions", "Security");

            entity.HasIndex(e => e.PermissionId, "IX_Security_RolePermissions_PermissionId");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_RolePermissions_Permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_RolePermissions_Roles");
        });

        modelBuilder.Entity<SafetyIncident>(entity =>
        {
            entity.HasKey(e => e.IncidentId).HasName("PK__SafetyIn__3D8053B2C7531BFF");

            entity.ToTable("SafetyIncidents", "HR");

            entity.HasIndex(e => e.EmployeeId, "IX_SafetyIncidents_EmployeeId");

            entity.HasIndex(e => e.SafetyTypeId, "IX_SafetyIncidents_SafetyTypeId");

            entity.Property(e => e.ActionTaken).HasMaxLength(1000);
            entity.Property(e => e.CostImpact)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DaysLost)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.InjuryType).HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(300);

            entity.HasOne(d => d.Employee).WithMany(p => p.SafetyIncidents)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SafetyIncidents_Employee");

            entity.HasOne(d => d.SafetyType).WithMany(p => p.SafetyIncidents)
                .HasForeignKey(d => d.SafetyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SafetyIncidents_SafetyType");
        });

        modelBuilder.Entity<SafetyType>(entity =>
        {
            entity.HasKey(e => e.SafetyTypeId).HasName("PK__SafetyTy__81905B9ABFFD0659");

            entity.ToTable("SafetyTypes", "HR");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SafetyTypeCode).HasMaxLength(50);
            entity.Property(e => e.SafetyTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.SafetyTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK__Sections__80EF087238ED383A");

            entity.ToTable("Sections", "HR");

            entity.HasIndex(e => e.SectionCode, "IX_HRSections_Code");

            entity.HasIndex(e => e.DepartmentId, "IX_Sections_DepartmentId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SectionCode).HasMaxLength(50);
            entity.Property(e => e.SectionNameAr).HasMaxLength(200);
            entity.Property(e => e.SectionNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Sections)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRSections_HRDepartments");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__Shift__C0A83881E685353D");

            entity.ToTable("Shift", "HR");

            entity.HasIndex(e => e.ShiftTypeId, "IX_Shift_ShiftTypeId");

            entity.Property(e => e.AllowLateDeduction).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ShiftCode).HasMaxLength(50);
            entity.Property(e => e.ShiftNameAr).HasMaxLength(200);
            entity.Property(e => e.ShiftNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.StandardHours).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.ShiftType).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.ShiftTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRShift_ShiftType");
        });

        modelBuilder.Entity<ShiftBreak>(entity =>
        {
            entity.HasKey(e => e.BreakId).HasName("PK__ShiftBre__B267A6392407487A");

            entity.ToTable("ShiftBreak", "HR");

            entity.HasIndex(e => e.ShiftId, "IX_ShiftBreak_ShiftId");

            entity.Property(e => e.BreakCode).HasMaxLength(50);
            entity.Property(e => e.BreakNameAr).HasMaxLength(200);
            entity.Property(e => e.BreakNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftBreaks)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRShiftBreak_Shift");
        });

        modelBuilder.Entity<ShiftPattern>(entity =>
        {
            entity.HasKey(e => e.PatternId).HasName("PK__ShiftPat__0A631B52776E628C");

            entity.ToTable("ShiftPattern", "HR");

            entity.Property(e => e.CycleDays).HasDefaultValue((short)7);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PatternCode).HasMaxLength(50);
            entity.Property(e => e.PatternNameAr).HasMaxLength(200);
            entity.Property(e => e.PatternNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ShiftPatternDetail>(entity =>
        {
            entity.HasKey(e => e.PatternDetailId).HasName("PK__ShiftPat__8244665B52202B3E");

            entity.ToTable("ShiftPatternDetail", "HR");

            entity.HasIndex(e => e.ShiftId, "IX_ShiftPatternDetail_ShiftId");

            entity.HasIndex(e => new { e.PatternId, e.DayNumber }, "UX_HRShiftPatternDetail_Pattern_Day").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Remarks).HasMaxLength(500);

            entity.HasOne(d => d.Pattern).WithMany(p => p.ShiftPatternDetails)
                .HasForeignKey(d => d.PatternId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRShiftPatternDetail_Pattern");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftPatternDetails)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_HRShiftPatternDetail_Shift");
        });

        modelBuilder.Entity<ShiftType>(entity =>
        {
            entity.HasKey(e => e.ShiftTypeId).HasName("PK__ShiftTyp__DCFDEDA9AEAA9293");

            entity.ToTable("ShiftType", "HR");

            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ShiftTypeCode).HasMaxLength(50);
            entity.Property(e => e.ShiftTypeNameAr).HasMaxLength(200);
            entity.Property(e => e.ShiftTypeNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Units__44F5ECB564B56107");

            entity.ToTable("Units", "HR");

            entity.HasIndex(e => e.UnitCode, "IX_HRUnits_Code");

            entity.HasIndex(e => e.SectionId, "IX_Units_SectionId");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UnitCode).HasMaxLength(50);
            entity.Property(e => e.UnitNameAr).HasMaxLength(200);
            entity.Property(e => e.UnitNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Section).WithMany(p => p.Units)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRUnits_HRSections");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Security_Users");

            entity.ToTable("Users", "Security");

            entity.HasIndex(e => e.Username, "UQ_Security_Users_Username").IsUnique();

            entity.HasIndex(e => e.Email, "UX_Security_Users_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginAt).HasPrecision(0);
            entity.Property(e => e.LockoutUntil).HasPrecision(0);
            entity.Property(e => e.PasswordChangedAt).HasPrecision(0);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAccess>(entity =>
        {
            entity.HasKey(e => e.UserAccessId).HasName("PK_Security_UserAccess");

            entity.ToTable("UserAccess", "Security");

            entity.HasIndex(e => e.CompanyBranchId, "IX_Security_UserAccess_CompanyBranchId");

            entity.HasIndex(e => e.CompanyId, "IX_Security_UserAccess_CompanyId");

            entity.HasIndex(e => e.UserId, "IX_Security_UserAccess_UserId");

            entity.HasIndex(e => new { e.CompanyId, e.CompanyBranchId }, "IX_UserAccess_CompanyId_CompanyBranchId");

            entity.HasIndex(e => new { e.UserId, e.CompanyBranchId }, "UX_Security_UserAccess_Branch")
                .IsUnique()
                .HasFilter("([CompanyBranchId] IS NOT NULL)");

            entity.HasIndex(e => new { e.UserId, e.CompanyId, e.CompanyBranchId }, "UX_Security_UserAccess_User_Company_Branch")
                .IsUnique()
                .HasFilter("([CompanyId] IS NOT NULL AND [CompanyBranchId] IS NOT NULL)");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Company).WithMany(p => p.UserAccesses)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK_Security_UserAccess_Companies");

            entity.HasOne(d => d.User).WithMany(p => p.UserAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_UserAccess_Users");

            entity.HasOne(d => d.CompanyBranch).WithMany(p => p.UserAccesses)
                .HasPrincipalKey(p => new { p.CompanyId, p.CompanyBranchId })
                .HasForeignKey(d => new { d.CompanyId, d.CompanyBranchId })
                .HasConstraintName("FK_Security_UserAccess_CompanyBranches");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserAccessId, e.RoleId }).HasName("PK_Security_UserRoles");

            entity.ToTable("UserRoles", "Security");

            entity.HasIndex(e => e.RoleId, "IX_Security_UserRoles_RoleId");

            entity.Property(e => e.AssignedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_UserRoles_Roles");

            entity.HasOne(d => d.UserAccess).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserAccessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security_UserRoles_UserAccess");
        });

        modelBuilder.Entity<WorkflowStepsConfig>(entity =>
        {
            entity.HasKey(e => e.StepConfigId).HasName("PK__Workflow__ACD167301884BF77");

            entity.ToTable("WorkflowStepsConfig", "HR");

            entity.HasIndex(e => e.ApproverTypeId, "IX_WorkflowStepsConfig_ApproverTypeId");

            entity.HasIndex(e => e.SpecificEmployeeId, "IX_WorkflowStepsConfig_SpecificEmployeeId");

            entity.HasIndex(e => e.SpecificPositionId, "IX_WorkflowStepsConfig_SpecificPositionId");

            entity.HasIndex(e => new { e.WorkflowTemplateId, e.StepOrder }, "UX_HRWorkflowStepsConfig_Template_Step")
                .IsUnique()
                .HasFilter("([IsActive]=(1))");

            entity.Property(e => e.AutoApproveDays).HasDefaultValue((short)0);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ApproverType).WithMany(p => p.WorkflowStepsConfigs)
                .HasForeignKey(d => d.ApproverTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRWorkflowSteps_ApproverType");

            entity.HasOne(d => d.SpecificEmployee).WithMany(p => p.WorkflowStepsConfigs)
                .HasForeignKey(d => d.SpecificEmployeeId)
                .HasConstraintName("FK_HRWorkflowSteps_SpecificEmployee");

            entity.HasOne(d => d.SpecificPosition).WithMany(p => p.WorkflowStepsConfigs)
                .HasForeignKey(d => d.SpecificPositionId)
                .HasConstraintName("FK_HRWorkflowSteps_Position");

            entity.HasOne(d => d.WorkflowTemplate).WithMany(p => p.WorkflowStepsConfigs)
                .HasForeignKey(d => d.WorkflowTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRWorkflowSteps_Template");
        });

        modelBuilder.Entity<WorkflowTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__Workflow__F87ADD277CD369CA");

            entity.ToTable("WorkflowTemplates", "HR");

            entity.HasIndex(e => e.ProcessTypeId, "IX_WorkflowTemplates_ProcessTypeId");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.TemplateCode).HasMaxLength(50);
            entity.Property(e => e.TemplateNameAr).HasMaxLength(200);
            entity.Property(e => e.TemplateNameEn)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ProcessType).WithMany(p => p.WorkflowTemplates)
                .HasForeignKey(d => d.ProcessTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HRWorkflowTemplates_Process");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
