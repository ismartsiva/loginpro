using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using loginprac.Data;
using loginprac.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("loginContextConnection") ?? throw new InvalidOperationException("Connection string 'loginContextConnection' not found.");

builder.Services.AddDbContext<loginContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<loginUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<loginContext>();


// Add services to the container.
builder.Services.AddIdentity<loginUser, IdentityRole>()
    .AddEntityFrameworkStores<loginContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );


app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    await Dbseeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();
