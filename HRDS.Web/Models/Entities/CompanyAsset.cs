using System;
using System.Collections.Generic;

namespace HRDS.Web.Models.Entities;

public partial class CompanyAsset
{
    public int AssetId { get; set; }

    public int AssetTypeId { get; set; }

    public string AssetCode { get; set; } = null!;

    public string AssetNameAr { get; set; } = null!;

    public string? AssetNameEn { get; set; }

    public string? SerialNumber { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public decimal? Cost { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsActive { get; set; }

    public int? CompanyId { get; set; }

    public int? CompanyBranchId { get; set; }

    public virtual AssetType? AssetType { get; set; } = null!;

    public virtual EmployeeAssetAssignment? EmployeeAssetAssignment { get; set; }
}
