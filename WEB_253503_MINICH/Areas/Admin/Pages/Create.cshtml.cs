using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ICupService _cupService;

        public CreateModel(ICategoryService categoryService, ICupService cupService)
        {
            _categoryService = categoryService;
            _cupService = cupService;
        }

        public IActionResult OnGet()
        {
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
                return Page();
            }

            await _cupService.CreateCupAsync(Cup, Image);

            return RedirectToPage("./Index");
        }
    }
}
