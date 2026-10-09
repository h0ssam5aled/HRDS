using System.Globalization;
using HRDS.Web.Models.Entities;
using HRDS.Web.Security;
using HRDS.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HRDS.Web.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 1. إضافة خدمات الترجمة وتحديد مجلد Resources
// ============================================================

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// ============================================================
// 2. إضافة MVC + View Localization + DataAnnotations  + Flexible Decimal Model Binder
// ============================================================

builder.Services.AddControllersWithViews(options =>
{
    // مهم جدًا:
    // نضع الـ Provider في أول القائمة حتى يتعامل مع
    // decimal و decimal? قبل الـ Default Model Binder.
    options.ModelBinderProviders.Insert(0, new FlexibleDecimalModelBinderProvider());
})
    .AddViewLocalization().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(HRDS.Web.Resources.Resource));
});

// ============================================================
// 3. تسجيل IHttpContextAccessor والـ Audit Interceptor
// ============================================================

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditSaveChangesInterceptor>();

// ============================================================
// 4. ربط قاعدة البيانات HRDSContext
// ============================================================

builder.Services.AddDbContext<HRDSContext>((sp, options) =>
{
    var interceptor = sp.GetRequiredService<AuditSaveChangesInterceptor>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).AddInterceptors(interceptor);
});

// ============================================================
// 5. إضافة نظام Cookie Authentication
// ============================================================

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Security/Account/Login";
        options.AccessDeniedPath = "/Security/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

// ============================================================
// 6. ضبط اللغات المدعومة
// ============================================================

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ar"),
        new CultureInfo("ar-EG"),
        new CultureInfo("en"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ar", "ar");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Clear();

    options.RequestCultureProviders.Add(new CookieRequestCultureProvider
    {
        CookieName = CookieRequestCultureProvider.DefaultCookieName
    });
});

// ============================================================
// 7. تسجيل نظام الصلاحيات المخصص
// ============================================================

builder.Services.AddSingleton<IAuthorizationPolicyProvider, ModulePolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, ModuleAccessHandler>();

// ============================================================
// 8. إضافة Session
// ============================================================

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ============================================================
// 9. HttpClient
// ============================================================

builder.Services.AddHttpClient();

// ============================================================
// Build Application
// ============================================================

var app = builder.Build();

// ============================================================
// 10. Error Handling / HSTS
// ============================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ============================================================
// 11. HTTPS
// ============================================================

app.UseHttpsRedirection();

// ============================================================
// 12. Static Files
// ============================================================

app.UseStaticFiles();

// ============================================================
// 13. Routing
// ============================================================

app.UseRouting();

// ============================================================
// 14. Localization  بعد Routing وقبل Authentication
// ============================================================

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(localizationOptions.Value);

// ============================================================
// 15. Session
// ============================================================

app.UseSession();

// ============================================================
// 16. Authentication
// ============================================================

app.UseAuthentication();

// ============================================================
// 17. Authorization
// ============================================================

app.UseAuthorization();

// ============================================================
// 18. Area Routes
// ============================================================

app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// ============================================================
// 19. Default Route
// ============================================================

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

// ============================================================
// Run
// ============================================================

app.Run();