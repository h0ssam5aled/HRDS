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
    public class BonusTypesController : Controller
    {
        private readonly HRDSContext _context;

        public BonusTypesController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.BonusTypes
                .Select(b => new BonusTypeViewModel
                {
                    BonusTypeId = b.BonusTypeId,
                    BonusTypeCode = b.BonusTypeCode,
                    BonusTypeNameAr = b.BonusTypeNameAr,
                    BonusTypeNameEn = b.BonusTypeNameEn,
                    IsTaxable = b.IsTaxable,
                    IsActive = b.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var b = await _context.BonusTypes.FindAsync(id);
            if (b == null) return NotFound();

            var model = new BonusTypeViewModel
            {
                BonusTypeId = b.BonusTypeId,
                BonusTypeCode = b.BonusTypeCode,
                BonusTypeNameAr = b.BonusTypeNameAr,
                BonusTypeNameEn = b.BonusTypeNameEn,
                IsTaxable = b.IsTaxable,
                IsActive = b.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] BonusTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");

                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.BonusTypeId == 0)
            {
                var entity = new BonusType
                {
                    BonusTypeCode = model.BonusTypeCode,
                    BonusTypeNameAr = model.BonusTypeNameAr,
                    BonusTypeNameEn = model.BonusTypeNameEn,
                    IsTaxable = model.IsTaxable,
                    IsActive = model.IsActive
                };
                _context.BonusTypes.Add(entity);
            }
            else
            {
                var entity = await _context.BonusTypes.FindAsync(model.BonusTypeId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.BonusTypeCode = model.BonusTypeCode;
                entity.BonusTypeNameAr = model.BonusTypeNameAr;
                entity.BonusTypeNameEn = model.BonusTypeNameEn;
                entity.IsTaxable = model.IsTaxable;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}