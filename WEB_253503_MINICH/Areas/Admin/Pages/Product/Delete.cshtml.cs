using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.UI.Data;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages.Product
{
    public class DeleteModel : PageModel
    {
        private readonly ICupService _cupService;
        public DeleteModel(ICupService cupService)
        {
            _cupService = cupService;
        }

        [BindProperty]
        public Cup Cup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseData = await _cupService.GetCupByIdAsync(id.Value);
            Cup = responseData.Data;

            if (Cup == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _cupService.DeleteCupAsync(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
