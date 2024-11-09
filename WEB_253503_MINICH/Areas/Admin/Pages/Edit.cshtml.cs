using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Data;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly ICupService _cupService;

        public EditModel(ICategoryService categoryService, ICupService cupService)
        {
            _categoryService = categoryService;
            _cupService = cupService;
        }

        [BindProperty]
        public Cup Cup { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cup = (await _cupService.GetCupByIdAsync(id.Value)).Data;
            if (cup == null)
            {
                return NotFound();
            }
            Cup = cup;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CupExists(Cup.Id).Result)
            {
                try
                {
                    await _cupService.UpdateCupAsync(Cup.Id, Cup, Image);
                }
                catch
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CupExists(int id)
        {
            return (await _cupService.GetCupByIdAsync(id)).Data != null;
        }
    }
}
