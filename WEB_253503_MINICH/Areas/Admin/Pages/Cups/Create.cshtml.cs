using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
/*using WEB_253503_MINICH.API.Data;*/
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages.Cups
{
    public class CreateModel : PageModel
    {
        private readonly ICupService _cupService;
        private readonly ICategoryService _categoryService;

        public CreateModel(ICupService cupService, ICategoryService categoryService)
        {
            _cupService = cupService;
            _categoryService = categoryService;
        }

        [BindProperty]
        public IEnumerable<Category> Categories { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> CategoryItems { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var responseData = await _categoryService.GetCategoryListAsync();
            Categories = responseData.Data!;
            CategoryItems = Categories.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = false
            });
            return Page();
        }

        [BindProperty]
        public Cup Cup { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error. Try again.");
                return Page();
            }

            await _cupService.CreateCupAsync(Cup, Image);

            return RedirectToPage("./Index");
        }
    }
}