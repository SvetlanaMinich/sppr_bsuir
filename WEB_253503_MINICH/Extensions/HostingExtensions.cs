using WEB_253503_MINICH.UI.Services.CategoryService;
using WEB_253503_MINICH.UI.Services.CupService;

namespace WEB_253503_MINICH.UI.Extensions
{
    public  static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<Services.CategoryService.ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<Services.CupService.ICupService, MemoryCupService>();
        }
    }
}
