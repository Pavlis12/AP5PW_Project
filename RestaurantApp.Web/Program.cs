using Microsoft.EntityFrameworkCore;
using RestaurantApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RestaurantDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Microsoft.AspNetCore.Identity.IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Pro vývoj nevyžadujeme potvrzení e-mailem
})
.AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>() // Aktivuje podporu pro roli Admin
.AddEntityFrameworkStores<RestaurantDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.Events.OnSignedIn = context =>
    {
        context.Response.Redirect("/Account/EnsureCustomerRole");
        return Task.CompletedTask;
    };
});

// Zpřístupní čtení cookies a HTTP kontextu
builder.Services.AddHttpContextAccessor();
// Propojí kolegův Interface s naší implementací
builder.Services.AddScoped<RestaurantApp.Application.Interfaces.ICurrentUserService, RestaurantApp.Web.Services.CurrentUserService>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(RestaurantApp.Application.Interfaces.IRepository<>), typeof(RestaurantApp.Infrastructure.Repositories.Repository<>));
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RestaurantApp.Infrastructure.DbInitializer.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Došlo k chybě při inicializaci databáze a zakládání účtů.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
