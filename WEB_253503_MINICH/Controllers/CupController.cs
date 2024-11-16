using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;


namespace WEB_253503_MINICH.UI.Controllers
{
    public class CupController : Controller
    {
        private readonly ICupService _cupService;
        private readonly ICategoryService _categoryService;
        public CupController(ICupService cupService, ICategoryService categoryService) 
        { 
            _cupService = cupService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(string? category, int? page = 1)
        {
            var cupResponse = await _cupService.GetCupListAsync(category, page.Value);
            var categories = await _categoryService.GetCategoryListAsync();
            if (!cupResponse.Successfull)
                return NotFound(cupResponse.ErrorMessage);

            var currentCategory = category != null ? categories.Data?.Find(g => g.NormalizedName!.Equals(category))?.Name : "All";
            ViewData["currentCategory"] = currentCategory;
            ViewData["categories"] = _categoryService.GetCategoryListAsync().Result.Data;
            ViewData["totalPages"] = cupResponse.Data!.TotalPages;

            return View(new ProductListModel<Cup> { Items = cupResponse.Data.Items, CurrentPage = page.Value, TotalPages = cupResponse.Data!.TotalPages });
        }
    }
}
