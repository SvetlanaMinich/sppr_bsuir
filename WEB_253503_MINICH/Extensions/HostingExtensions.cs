using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.UI.Services.FileService;

namespace WEB_253503_MINICH.UI.Extensions
{
    public  static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, ApiCategoryService>();
            builder.Services.AddScoped<ICupService, ApiCupService>();
            builder.Services.AddScoped<IFileService, ApiFileService>();
        }
    }
}
