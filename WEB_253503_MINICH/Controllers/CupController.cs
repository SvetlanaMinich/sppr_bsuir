using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public async Task<IActionResult> Index(string? category, [FromQuery]int page)
        {
            int pageNumber = page;
            var categories = await _categoryService.GetCategoryListAsync();
            var currentCategory = category != null ? categories.Data?.Find(g => g.NormalizedName!.Equals(category))?.Name : "All";
            ViewData["currentCategory"] = currentCategory;
            ViewData["categories"] = _categoryService.GetCategoryListAsync().Result.Data;

            var cupResponse = await _cupService.GetCupListAsync(category, page);
            if (!cupResponse.Successfull)
                return NotFound(cupResponse.ErrorMessage);
            ViewData["totalPages"] = cupResponse.Data!.TotalPages;

            return View(new ProductListModel<Cup> { Items = cupResponse.Data.Items, CurrentPage = page, TotalPages = cupResponse.Data!.TotalPages });
        }
    }
}
