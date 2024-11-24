using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.HelperClasses;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.UI.Services.Authentication;
using WEB_253503_MINICH.UI.Services.Authorization;
using WEB_253503_MINICH.UI.Services.FileService;
using WEB_253503_MINICH.UI.Sessions;

namespace WEB_253503_MINICH.UI.Extensions
{
    public  static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, ApiCategoryService>();
            builder.Services.AddScoped<ICupService, ApiCupService>();
            builder.Services.AddScoped<IFileService, ApiFileService>();
            builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
            builder.Services.AddScoped<ITokenAccessor, KeycloakTokenAccessor>();
            builder.Services.AddScoped<IAuthService, KeycloakAuthService>();
            builder.Services.AddScoped<Cart, SessionCart>();
        }
    }
}
