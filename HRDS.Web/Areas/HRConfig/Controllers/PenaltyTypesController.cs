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
    public class PenaltyTypesController : Controller
    {
        private readonly HRDSContext _context;

        public PenaltyTypesController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.PenaltyTypes
                .Select(p => new PenaltyTypeViewModel
                {
                    PenaltyTypeId = p.PenaltyTypeId,
                    PenaltyTypeCode = p.PenaltyTypeCode,
                    PenaltyTypeNameAr = p.PenaltyTypeNameAr,
                    PenaltyTypeNameEn = p.PenaltyTypeNameEn,
                    DefaultDeductionDays = p.DefaultDeductionDays,
                    IsActive = p.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _context.PenaltyTypes.FindAsync(id);
            if (p == null) return NotFound();

            var model = new PenaltyTypeViewModel
            {
                PenaltyTypeId = p.PenaltyTypeId,
                PenaltyTypeCode = p.PenaltyTypeCode,
                PenaltyTypeNameAr = p.PenaltyTypeNameAr,
                PenaltyTypeNameEn = p.PenaltyTypeNameEn,
                DefaultDeductionDays = p.DefaultDeductionDays,
                IsActive = p.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] PenaltyTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.PenaltyTypeId == 0)
            {
                var entity = new PenaltyType
                {
                    PenaltyTypeCode = model.PenaltyTypeCode,
                    PenaltyTypeNameAr = model.PenaltyTypeNameAr,
                    PenaltyTypeNameEn = model.PenaltyTypeNameEn,
                    DefaultDeductionDays = model.DefaultDeductionDays,
                    IsActive = model.IsActive
                };
                _context.PenaltyTypes.Add(entity);
            }
            else
            {
                var entity = await _context.PenaltyTypes.FindAsync(model.PenaltyTypeId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.PenaltyTypeCode = model.PenaltyTypeCode;
                entity.PenaltyTypeNameAr = model.PenaltyTypeNameAr;
                entity.PenaltyTypeNameEn = model.PenaltyTypeNameEn;
                entity.DefaultDeductionDays = model.DefaultDeductionDays;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}