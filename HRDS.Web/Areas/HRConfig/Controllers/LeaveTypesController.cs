using HRDS.Web.Areas.HRConfig.ViewModels;
using HRDS.Web.Models;
using HRDS.Web.Models.Entities;
using HRDS.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HRDS.Web.Areas.HRConfig.Controllers
{
    [Authorize]
    [Area("HRConfig")]
    [HasModuleAccess("HRConfig")]
    public class LeaveTypesController : Controller
    {
        private readonly HRDSContext _context;

        public LeaveTypesController(HRDSContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // تم تصحيح اسم الحقل إلى LeaveCategoryNameAr
            ViewBag.Categories = new SelectList(
                await _context.LeaveCategories.Where(c => c.IsActive && !c.IsDeleted).ToListAsync(),
                "LeaveCategoryId",
                "LeaveCategoryNameAr"
            );
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.LeaveTypes
                .Include(l => l.LeaveCategory)
                .Select(l => new LeaveTypeViewModel
                {
                    LeaveTypeId = l.LeaveTypeId,
                    LeaveCode = l.LeaveCode,
                    LeaveNameAr = l.LeaveNameAr,
                    LeaveNameEn = l.LeaveNameEn,
                    LeaveCategoryId = l.LeaveCategoryId,
                    LeaveCategoryName = l.LeaveCategory != null ? l.LeaveCategory.LeaveCategoryNameAr : "-",
                    IsPaid = l.IsPaid,
                    RequiresBalance = l.RequiresBalance,
                    MaxDaysPerYear = l.MaxDaysPerYear,
                    IsActive = l.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var l = await _context.LeaveTypes.FindAsync(id);
            if (l == null) return NotFound();

            var model = new LeaveTypeViewModel
            {
                LeaveTypeId = l.LeaveTypeId,
                LeaveCode = l.LeaveCode,
                LeaveNameAr = l.LeaveNameAr,
                LeaveNameEn = l.LeaveNameEn,
                LeaveCategoryId = l.LeaveCategoryId,
                IsPaid = l.IsPaid,
                RequiresBalance = l.RequiresBalance,
                RequiresAttachment = l.RequiresAttachment,
                RequiresApproval = l.RequiresApproval,
                RequiresWorkflow = l.RequiresWorkflow,
                DeductFromSalary = l.DeductFromSalary,
                MaxDaysPerRequest = l.MaxDaysPerRequest,
                MaxDaysPerYear = l.MaxDaysPerYear,
                MinimumDaysPerRequest = l.MinimumDaysPerRequest,
                MaximumConsecutiveDays = l.MaximumConsecutiveDays,
                AllowCarryForward = l.AllowCarryForward,
                CarryForwardLimit = l.CarryForwardLimit,
                ExpireAtYearEnd = l.ExpireAtYearEnd,
                AllowBackDateRequest = l.AllowBackDateRequest,
                AllowFutureRequest = l.AllowFutureRequest,
                MaxFutureDays = l.MaxFutureDays,
                ColorCode = l.ColorCode,
                IconName = l.IconName,
                DisplayOrder = l.DisplayOrder,
                IsActive = l.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] LeaveTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.LeaveTypeId == 0)
            {
                var entity = new LeaveType
                {
                    LeaveCode = model.LeaveCode,
                    LeaveNameAr = model.LeaveNameAr,
                    LeaveNameEn = model.LeaveNameEn,
                    LeaveCategoryId = model.LeaveCategoryId,
                    IsPaid = model.IsPaid,
                    RequiresBalance = model.RequiresBalance,
                    RequiresAttachment = model.RequiresAttachment,
                    RequiresApproval = model.RequiresApproval,
                    RequiresWorkflow = model.RequiresWorkflow,
                    DeductFromSalary = model.DeductFromSalary,
                    MaxDaysPerRequest = model.MaxDaysPerRequest,
                    MaxDaysPerYear = model.MaxDaysPerYear,
                    MinimumDaysPerRequest = model.MinimumDaysPerRequest,
                    MaximumConsecutiveDays = model.MaximumConsecutiveDays,
                    AllowCarryForward = model.AllowCarryForward,
                    CarryForwardLimit = model.CarryForwardLimit,
                    ExpireAtYearEnd = model.ExpireAtYearEnd,
                    AllowBackDateRequest = model.AllowBackDateRequest,
                    AllowFutureRequest = model.AllowFutureRequest,
                    MaxFutureDays = model.MaxFutureDays,
                    ColorCode = model.ColorCode,
                    IconName = model.IconName,
                    DisplayOrder = model.DisplayOrder,
                    IsActive = model.IsActive
                };
                _context.LeaveTypes.Add(entity);
            }
            else
            {
                var entity = await _context.LeaveTypes.FindAsync(model.LeaveTypeId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.LeaveCode = model.LeaveCode;
                entity.LeaveNameAr = model.LeaveNameAr;
                entity.LeaveNameEn = model.LeaveNameEn;
                entity.LeaveCategoryId = model.LeaveCategoryId;
                entity.IsPaid = model.IsPaid;
                entity.RequiresBalance = model.RequiresBalance;
                entity.RequiresAttachment = model.RequiresAttachment;
                entity.RequiresApproval = model.RequiresApproval;
                entity.RequiresWorkflow = model.RequiresWorkflow;
                entity.DeductFromSalary = model.DeductFromSalary;
                entity.MaxDaysPerRequest = model.MaxDaysPerRequest;
                entity.MaxDaysPerYear = model.MaxDaysPerYear;
                entity.MinimumDaysPerRequest = model.MinimumDaysPerRequest;
                entity.MaximumConsecutiveDays = model.MaximumConsecutiveDays;
                entity.AllowCarryForward = model.AllowCarryForward;
                entity.CarryForwardLimit = model.CarryForwardLimit;
                entity.ExpireAtYearEnd = model.ExpireAtYearEnd;
                entity.AllowBackDateRequest = model.AllowBackDateRequest;
                entity.AllowFutureRequest = model.AllowFutureRequest;
                entity.MaxFutureDays = model.MaxFutureDays;
                entity.ColorCode = model.ColorCode;
                entity.IconName = model.IconName;
                entity.DisplayOrder = model.DisplayOrder;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}