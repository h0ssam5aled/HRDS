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
    public class ExpenseTypesController : Controller
    {
        private readonly HRDSContext _context;

        public ExpenseTypesController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.ExpenseTypes
                .Where(e => !e.IsDeleted)
                .Select(e => new ExpenseTypeViewModel
                {
                    ExpenseTypeId = e.ExpenseTypeId,
                    ExpenseTypeCode = e.ExpenseTypeCode,
                    ExpenseTypeNameAr = e.ExpenseTypeNameAr,
                    ExpenseTypeNameEn = e.ExpenseTypeNameEn,
                    Description = e.Description,
                    MaxLimit = e.MaxLimit,
                    RequiresAttachment = e.RequiresAttachment,
                    IsActive = e.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var e = await _context.ExpenseTypes.FindAsync(id);
            if (e == null || e.IsDeleted) return NotFound();

            var model = new ExpenseTypeViewModel
            {
                ExpenseTypeId = e.ExpenseTypeId,
                ExpenseTypeCode = e.ExpenseTypeCode,
                ExpenseTypeNameAr = e.ExpenseTypeNameAr,
                ExpenseTypeNameEn = e.ExpenseTypeNameEn,
                Description = e.Description,
                MaxLimit = e.MaxLimit,
                RequiresAttachment = e.RequiresAttachment,
                IsActive = e.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] ExpenseTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.ExpenseTypeId == 0)
            {
                var entity = new ExpenseType
                {
                    ExpenseTypeCode = model.ExpenseTypeCode,
                    ExpenseTypeNameAr = model.ExpenseTypeNameAr,
                    ExpenseTypeNameEn = model.ExpenseTypeNameEn,
                    Description = model.Description,
                    MaxLimit = model.MaxLimit,
                    RequiresAttachment = model.RequiresAttachment,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };
                _context.ExpenseTypes.Add(entity);
            }
            else
            {
                var entity = await _context.ExpenseTypes.FindAsync(model.ExpenseTypeId);
                if (entity == null || entity.IsDeleted) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.ExpenseTypeCode = model.ExpenseTypeCode;
                entity.ExpenseTypeNameAr = model.ExpenseTypeNameAr;
                entity.ExpenseTypeNameEn = model.ExpenseTypeNameEn;
                entity.Description = model.Description;
                entity.MaxLimit = model.MaxLimit;
                entity.RequiresAttachment = model.RequiresAttachment;
                entity.IsActive = model.IsActive;
                entity.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}