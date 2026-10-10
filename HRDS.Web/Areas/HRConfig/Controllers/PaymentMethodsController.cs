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
    public class PaymentMethodsController : Controller
    {
        private readonly HRDSContext _context;

        public PaymentMethodsController(HRDSContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetJson()
        {
            var data = await _context.PaymentMethods
                .Select(p => new PaymentMethodViewModel
                {
                    PaymentMethodId = p.PaymentMethodId,
                    PaymentMethodCode = p.PaymentMethodCode,
                    PaymentMethodNameAr = p.PaymentMethodNameAr,
                    PaymentMethodNameEn = p.PaymentMethodNameEn,
                    Description = p.Description,
                    IsActive = p.IsActive
                }).ToListAsync();

            return Json(new { data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _context.PaymentMethods.FindAsync(id);
            if (p == null) return NotFound();

            var model = new PaymentMethodViewModel
            {
                PaymentMethodId = p.PaymentMethodId,
                PaymentMethodCode = p.PaymentMethodCode,
                PaymentMethodNameAr = p.PaymentMethodNameAr,
                PaymentMethodNameEn = p.PaymentMethodNameEn,
                Description = p.Description,
                IsActive = p.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] PaymentMethodViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .Select(x => $"{x.Key}: {x.Value!.Errors.FirstOrDefault()?.ErrorMessage}");
                return Json(new { success = false, message = "بيانات غير صالحة: " + string.Join(" | ", errors) });
            }

            if (model.PaymentMethodId == 0)
            {
                var entity = new PaymentMethod
                {
                    PaymentMethodCode = model.PaymentMethodCode,
                    PaymentMethodNameAr = model.PaymentMethodNameAr,
                    PaymentMethodNameEn = model.PaymentMethodNameEn,
                    Description = model.Description,
                    IsActive = model.IsActive
                };
                _context.PaymentMethods.Add(entity);
            }
            else
            {
                var entity = await _context.PaymentMethods.FindAsync(model.PaymentMethodId);
                if (entity == null) return Json(new { success = false, message = "العنصر غير موجود" });

                entity.PaymentMethodCode = model.PaymentMethodCode;
                entity.PaymentMethodNameAr = model.PaymentMethodNameAr;
                entity.PaymentMethodNameEn = model.PaymentMethodNameEn;
                entity.Description = model.Description;
                entity.IsActive = model.IsActive;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}