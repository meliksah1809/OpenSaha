using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => { 
    x.LoginPath = "/Kayit";
    //x.ExpireTimeSpan = TimeSpan.FromSeconds(60);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserYonetici", policy =>
    {
        policy.RequireClaim("UserType", UserType.SahaYonetici.ToString(),UserType.Yonetici.ToString());
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Yonetici", policy =>
    {
        policy.RequireClaim("UserType", UserType.Yonetici.ToString());
    });
});

builder.Services.AddDbContext<SahaContext>(opt =>
{
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultConnectionMySQL"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnectionMySQL")));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();