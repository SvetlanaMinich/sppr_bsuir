using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.UI.Data;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly ICupService _CupService;
        private readonly ICategoryService _CategoryService;

        public EditModel(ICupService CupService, ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
            _CupService = CupService;
        }

        [BindProperty]
        public Cup Cup { get; set; } = default!;

        [BindProperty]
        public IEnumerable<Category> Categorys { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public IEnumerable<SelectListItem> CategoryItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseDataCup = await _CupService.GetCupByIdAsync(id.Value);
            Cup = responseDataCup.Data;
            if (Cup == null)
            {
                return NotFound();
            }

            var responseDataCategory = await _CategoryService.GetCategoryListAsync();
            Categorys = responseDataCategory.Data!;
            CategoryItems = Categorys.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Name,
                Selected = g.Id == Cup.Category!.Id ? true : false
            });

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error. Try again.");
                return Page();
            }

            try
            {
                await _CupService.UpdateCupAsync(Cup.Id, Cup, Image);
            }
            catch
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
