using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.API.Data;
using WEB_253503_MINICH.API.Services.CategoryService;
using WEB_253503_MINICH.API.Services.CupService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICupService, CupService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionStr = builder.Configuration.GetConnectionString("default")!;
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionStr));

var app = builder.Build();

await DbInitializer.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(); //using static files like images

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/*app.MapControllerRoute(
    name: "cups",
    pattern: "catalog/{category?}/{page?}",
    defaults: new { controller = "Cup", action = "Index" }
);*/

app.Run();
