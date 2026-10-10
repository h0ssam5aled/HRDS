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
    public class BusinessMissionTypesController : Controller
    {
        private readonly HRDSContext _context;

        public BusinessMissionTypesController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.BusinessMissionTypes
                .Select(m => new BusinessMissionTypeViewModel
                {
                    MissionTypeId = m.MissionTypeId,
                    MissionTypeCode = m.MissionTypeCode,
                    MissionTypeNameAr = m.MissionTypeNameAr,
                    MissionTypeNameEn = m.MissionTypeNameEn,
                    HasAllowance = m.HasAllowance,
                    IsActive = m.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var m = await _context.BusinessMissionTypes.FindAsync(id);
            if (m == null) return NotFound();

            var model = new BusinessMissionTypeViewModel
            {
                MissionTypeId = m.MissionTypeId,
                MissionTypeCode = m.MissionTypeCode,
                MissionTypeNameAr = m.MissionTypeNameAr,
                MissionTypeNameEn = m.MissionTypeNameEn,
                HasAllowance = m.HasAllowance,
                IsActive = m.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] BusinessMissionTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.MissionTypeId == 0)
            {
                var entity = new BusinessMissionType
                {
                    MissionTypeCode = model.MissionTypeCode,
                    MissionTypeNameAr = model.MissionTypeNameAr,
                    MissionTypeNameEn = model.MissionTypeNameEn,
                    HasAllowance = model.HasAllowance,
                    IsActive = model.IsActive
                };
                _context.BusinessMissionTypes.Add(entity);
            }
            else
            {
                var entity = await _context.BusinessMissionTypes.FindAsync(model.MissionTypeId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.MissionTypeCode = model.MissionTypeCode;
                entity.MissionTypeNameAr = model.MissionTypeNameAr;
                entity.MissionTypeNameEn = model.MissionTypeNameEn;
                entity.HasAllowance = model.HasAllowance;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}