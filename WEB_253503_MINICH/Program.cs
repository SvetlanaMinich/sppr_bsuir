using WEB_253503_MINICH.UI.Data;
using WEB_253503_MINICH.UI.Extensions;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.UI.Models;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));
var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();

builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri(uriData.ApiUri);
});

string connectionStr = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionStr));

// Registration new services
builder.RegisterCustomServices();

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AreaAdmin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
