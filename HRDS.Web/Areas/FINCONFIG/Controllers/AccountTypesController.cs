using System.Globalization;
using HRDS.Web.Areas.Finance.ViewModels;
using HRDS.Web.Models;
using HRDS.Web.Models.Entities;
using HRDS.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRDS.Web.Controllers
{
    [Authorize]
    [Area("FINCONFIG")]
    [HasModuleAccess("FINCONFIG")]
    public class AccountTypesController : Controller
    {
        private readonly HRDSContext _context;

        public AccountTypesController(HRDSContext context)
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
            var isEn = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower() == "en";

            var rawData = await _context.AccountTypes
                .OrderByDescending(x => x.AccountTypeId)
                .ToListAsync();

            var list = rawData.Select(x => new AccountTypeViewModel
            {
                AccountTypeId = x.AccountTypeId,
                Code = x.AccountTypeCode,
                AccountTypeNameAr = x.AccountTypeNameAr,
                AccountTypeNameEn = x.AccountTypeNameEn,
                DisplayAccountTypeName = isEn && !string.IsNullOrEmpty(x.AccountTypeNameEn) ? x.AccountTypeNameEn : x.AccountTypeNameAr,
                Description = x.Description,
                IsActive = x.IsActive
            }).ToList();

            return Json(new { data = list });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.AccountTypeId == id);
            if (entity == null) return Json(null);

            var model = new AccountTypeViewModel
            {
                AccountTypeId = entity.AccountTypeId,
                Code = entity.AccountTypeCode,
                AccountTypeNameAr = entity.AccountTypeNameAr,
                AccountTypeNameEn = entity.AccountTypeNameEn,
                Description = entity.Description,
                IsActive = entity.IsActive
            };

            return Json(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] AccountTypeViewModel model)
        {
            if (!ModelState.IsValid) return Json(new { success = false, message = "Invalid Data" });

            if (model.AccountTypeId == 0)
            {
                var entity = new AccountType
                {
                    AccountTypeCode = model.Code.ToUpper().Trim(),
                    AccountTypeNameAr = model.AccountTypeNameAr.Trim(),
                    AccountTypeNameEn = model.AccountTypeNameEn?.Trim(),
                    Description = model.Description?.Trim(),
                    IsActive = model.IsActive,
                    CreatedDate = DateTime.Now
                };
                _context.AccountTypes.Add(entity);
            }
            else
            {
                var entity = await _context.AccountTypes.FirstOrDefaultAsync(x => x.AccountTypeId == model.AccountTypeId);
                if (entity == null) return Json(new { success = false, message = "Record Not Found" });

                entity.AccountTypeCode = model.Code.ToUpper().Trim();
                entity.AccountTypeNameAr = model.AccountTypeNameAr.Trim();
                entity.AccountTypeNameEn = model.AccountTypeNameEn?.Trim();
                entity.Description = model.Description?.Trim();
                entity.IsActive = model.IsActive;
                entity.ModifiedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Saved Successfully" });
        }
    }
}