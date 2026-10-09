using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class AccountType
{
    public int AccountTypeId { get; set; }

    public string AccountTypeCode { get; set; } = null!;

    public string AccountTypeNameAr { get; set; } = null!;

    public string? AccountTypeNameEn { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
