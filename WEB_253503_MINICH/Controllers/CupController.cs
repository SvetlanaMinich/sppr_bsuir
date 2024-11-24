using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.UI.Extensions;


namespace WEB_253503_MINICH.UI.Controllers
{
    [Route("Catalog")]
    public class CupController : Controller
    {
        private readonly ICupService _cupService;
        private readonly ICategoryService _categoryService;
        public CupController(ICupService cupService, ICategoryService categoryService) 
        { 
            _cupService = cupService;
            _categoryService = categoryService;
        }

        [Route("")]
        [Route("{category?}")]
        public async Task<IActionResult> Index(string? category, int? pageNo = 1)
        {
            
            var categories = await _categoryService.GetCategoryListAsync();
            if (!categories.Successfull)
                return NotFound(categories.ErrorMessage);

            var currentCategory = category != null ? categories.Data?.Find(g => g.NormalizedName!.Equals(category))?.Name : "All";
            ViewData["currentCategory"] = currentCategory;
            ViewData["categories"] = _categoryService.GetCategoryListAsync().Result.Data;

            var cupResponse = await _cupService.GetCupListAsync(category, pageNo.Value);
            if (!cupResponse.Successfull)
                return NotFound(cupResponse.ErrorMessage);

            ViewData["totalPages"] = cupResponse.Data!.TotalPages;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CupListPartial", cupResponse.Data);
            }

            return View(cupResponse.Data);
        }
    }
}
