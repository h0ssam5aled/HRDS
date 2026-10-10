using HRDS.Web.Areas.HRConfig.ViewModels;
using HRDS.Web.Models;
using HRDS.Web.Models.Entities;
using HRDS.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRDS.Web.Areas.HRConfig.Controllers
{
    [Authorize]
    [Area("HRConfig")]
    [HasModuleAccess("HRConfig")]
    public class PermissionTypesController : Controller
    {
        private readonly HRDSContext _context;

        public PermissionTypesController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.PermissionTypes
                .Select(p => new PermissionTypeViewModel
                {
                    PermissionTypeId = p.PermissionTypeId,
                    PermissionTypeCode = p.PermissionTypeCode,
                    PermissionTypeNameAr = p.PermissionTypeNameAr,
                    PermissionTypeNameEn = p.PermissionTypeNameEn,
                    MaxHoursPerMonth = p.MaxHoursPerMonth,
                    MaxCountPerMonth = p.MaxCountPerMonth,
                    DeductFromSalary = p.DeductFromSalary,
                    DeductFromLeaveBalance = p.DeductFromLeaveBalance,
                    RequiresAttachment = p.RequiresAttachment,
                    IsActive = p.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _context.PermissionTypes.FindAsync(id);
            if (p == null) return NotFound();

            var model = new PermissionTypeViewModel
            {
                PermissionTypeId = p.PermissionTypeId,
                PermissionTypeCode = p.PermissionTypeCode,
                PermissionTypeNameAr = p.PermissionTypeNameAr,
                PermissionTypeNameEn = p.PermissionTypeNameEn,
                MaxHoursPerMonth = p.MaxHoursPerMonth,
                MaxCountPerMonth = p.MaxCountPerMonth,
                DeductFromSalary = p.DeductFromSalary,
                DeductFromLeaveBalance = p.DeductFromLeaveBalance,
                RequiresAttachment = p.RequiresAttachment,
                IsActive = p.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] PermissionTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.PermissionTypeId == 0)
            {
                var entity = new PermissionType
                {
                    PermissionTypeCode = model.PermissionTypeCode,
                    PermissionTypeNameAr = model.PermissionTypeNameAr,
                    PermissionTypeNameEn = model.PermissionTypeNameEn,
                    MaxHoursPerMonth = model.MaxHoursPerMonth,
                    MaxCountPerMonth = model.MaxCountPerMonth,
                    DeductFromSalary = model.DeductFromSalary,
                    DeductFromLeaveBalance = model.DeductFromLeaveBalance,
                    RequiresAttachment = model.RequiresAttachment,
                    IsActive = model.IsActive
                };
                _context.PermissionTypes.Add(entity);
            }
            else
            {
                var entity = await _context.PermissionTypes.FindAsync(model.PermissionTypeId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.PermissionTypeCode = model.PermissionTypeCode;
                entity.PermissionTypeNameAr = model.PermissionTypeNameAr;
                entity.PermissionTypeNameEn = model.PermissionTypeNameEn;
                entity.MaxHoursPerMonth = model.MaxHoursPerMonth;
                entity.MaxCountPerMonth = model.MaxCountPerMonth;
                entity.DeductFromSalary = model.DeductFromSalary;
                entity.DeductFromLeaveBalance = model.DeductFromLeaveBalance;
                entity.RequiresAttachment = model.RequiresAttachment;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}