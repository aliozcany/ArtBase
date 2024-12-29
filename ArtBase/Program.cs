using ArtBase.Models;
using ArtBase.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MySQL ba�lant�s�n� ArtBaseDBContext ile yap�land�r
builder.Services.AddDbContext<ArtBaseDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6; // �ifre minimum uzunlu�u 6 karakter
    options.Password.RequireDigit = false; // Rakam gereksinimi kapal�
    options.Password.RequireUppercase = false; // B�y�k harf gereksinimi kapal�
    options.Password.RequireLowercase = false; // K���k harf gereksinimi kapal�
    options.Password.RequireNonAlphanumeric = false; // �zel karakter gereksinimi kapal�
})
.AddEntityFrameworkStores<ArtBaseDBContext>()
.AddDefaultTokenProviders();

builder.Services.AddHttpClient<TmdbService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization middleware'lerini ekle
app.UseAuthentication(); // Kimlik do�rulama
app.UseAuthorization();  // Yetkilendirme


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
