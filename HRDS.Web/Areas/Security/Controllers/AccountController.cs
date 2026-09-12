using HRDS.Web.Areas.Security.Models;
using HRDS.Web.Models.Entities;
using HRDS.Web.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace HRDS.Web.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly HRDSContext _context; // استبدل ApplicationDbContext باسم الـ DbContext لديك
        private readonly IConfiguration _configuration;

        // حقن التبعيات عبر الـ Constructor
        public AccountController(HRDSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var superAdminUser = _configuration["SuperAdmin:Username"];
            var superAdminHash = _configuration["SuperAdmin:PasswordHash"];

            // 1. التحقق من حساب الـ SuperAdmin عبر الـ Hash المخزن
            if (!string.IsNullOrEmpty(superAdminUser) &&
                string.Equals(model.Username, superAdminUser, StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrEmpty(superAdminHash))
            {
                var hasher = new PasswordHasher<object>();

                var result = hasher.VerifyHashedPassword(new object(), superAdminHash, model.Password ?? string.Empty);

                if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, "0"),
                            new Claim(ClaimTypes.Name, superAdminUser),
                            new Claim(ClaimTypes.Role, "SuperAdmin"),
                            new Claim("IsSuperAdmin", "True")
                        };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            // 2. التحقق من مستخدم قاعدة البيانات
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.IsActive);

            if (user != null)
            {
                // فحص حالة الإغلاق Lockout
                if (user.LockoutUntil.HasValue && user.LockoutUntil.Value > DateTime.UtcNow)
                {
                    ModelState.AddModelError(string.Empty, "الحساب مغلق مؤقتاً لكثرة المحاولات الخاطئة.");
                    return View(model);
                }

                var hasher = new PasswordHasher<object>();
                var verifyResult = hasher.VerifyHashedPassword(new object(), user.PasswordHash, model.Password ?? string.Empty);

                if (verifyResult == PasswordVerificationResult.Success || verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    // إعادة تصفية محاولات الفشل وتحديث تاريخ آخر دخول
                    user.FailedLoginCount = 0;
                    user.LastLoginAt = DateTime.UtcNow;
                    user.LockoutUntil = null;
                    await _context.SaveChangesAsync();

                    // جلب صلاحية الوصول الافتراضية للشركة والفرع (UserAccess)
                    var userAccess = await _context.UserAccesses
                        .FirstOrDefaultAsync(a => a.UserId == user.UserId && a.IsDefault && a.IsActive);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("CompanyId", userAccess?.CompanyId?.ToString() ?? "0"),
                        new Claim("CompanyBranchId", userAccess?.CompanyBranchId?.ToString() ?? "0"),
                        new Claim("UserAccessId", userAccess?.UserAccessId.ToString() ?? "0")
                    };

                    // إضافة الأدوار (Roles) التابعة لـ UserAccess الخاص به
                    if (userAccess != null)
                    {
                        var roleNames = await _context.UserRoles
                            .Where(ur => ur.UserAccessId == userAccess.UserAccessId && ur.IsActive)
                            .Select(ur => ur.Role.RoleCode)
                            .ToListAsync();

                        foreach (var role in roleNames)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    // زيادة عداد الفشل وإغلاق الحساب عند تجاوز عدد محدد (مثل 5 محاولات)
                    user.FailedLoginCount++;
                    if (user.FailedLoginCount >= 5)
                    {
                        user.LockoutUntil = DateTime.UtcNow.AddMinutes(15); // إغلاق لمدة 15 دقيقة
                    }
                    await _context.SaveChangesAsync();
                }
            }

            string _err = Resource.InvalidUsernameOrPassword;
            ModelState.AddModelError(string.Empty, _err);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // التوجيه لشاشة تسجيل الدخول داخل نفس الـ Area
            return RedirectToAction("Login", "Account", new { area = "Security" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}