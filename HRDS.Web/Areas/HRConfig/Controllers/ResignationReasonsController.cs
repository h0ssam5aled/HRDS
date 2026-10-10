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
    public class ResignationReasonsController : Controller
    {
        private readonly HRDSContext _context;

        public ResignationReasonsController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.ResignationReasons
                .Where(r => !r.IsDeleted)
                .Select(r => new ResignationReasonViewModel
                {
                    ResignationReasonId = r.ResignationReasonId,
                    ResignationReasonCode = r.ResignationReasonCode,
                    ResignationReasonNameAr = r.ResignationReasonNameAr,
                    ResignationReasonNameEn = r.ResignationReasonNameEn,
                    Description = r.Description,
                    IsActive = r.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _context.ResignationReasons.FindAsync(id);
            if (r == null || r.IsDeleted) return NotFound();

            var model = new ResignationReasonViewModel
            {
                ResignationReasonId = r.ResignationReasonId,
                ResignationReasonCode = r.ResignationReasonCode,
                ResignationReasonNameAr = r.ResignationReasonNameAr,
                ResignationReasonNameEn = r.ResignationReasonNameEn,
                Description = r.Description,
                IsActive = r.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] ResignationReasonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.ResignationReasonId == 0)
            {
                var entity = new ResignationReason
                {
                    ResignationReasonCode = model.ResignationReasonCode,
                    ResignationReasonNameAr = model.ResignationReasonNameAr,
                    ResignationReasonNameEn = model.ResignationReasonNameEn,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };
                _context.ResignationReasons.Add(entity);
            }
            else
            {
                var entity = await _context.ResignationReasons.FindAsync(model.ResignationReasonId);
                if (entity == null || entity.IsDeleted) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.ResignationReasonCode = model.ResignationReasonCode;
                entity.ResignationReasonNameAr = model.ResignationReasonNameAr;
                entity.ResignationReasonNameEn = model.ResignationReasonNameEn;
                entity.Description = model.Description;
                entity.IsActive = model.IsActive;
                entity.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}