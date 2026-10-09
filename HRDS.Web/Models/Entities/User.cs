using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? LastLoginAt { get; set; }

    public DateTime? PasswordChangedAt { get; set; }

    public int FailedLoginCount { get; set; }

    public DateTime? LockoutUntil { get; set; }

    public bool IsActive { get; set; }

    public int? CompanyId { get; set; }

    public int? CompanyBranchId { get; set; }

    public virtual ICollection<UserAccess> UserAccesses { get; set; } = new List<UserAccess>();
}
