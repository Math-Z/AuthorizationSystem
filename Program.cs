using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthorizationSystem.Data;
using AuthorizationSystem.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthorizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthorizationDbContextConnection' not found.");

builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthorizationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Suporte para Razor Pages.
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
