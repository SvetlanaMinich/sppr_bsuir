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
    public class DetailsModel : PageModel
    {
        private readonly ICupService _CupService;

        public DetailsModel(ICupService CupSevice)
        {
            _CupService = CupSevice;
        }

        public Cup Cup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responseData = await _CupService.GetCupByIdAsync(id.Value);
            if (!responseData.Successfull)
            {
                return NotFound();
            }
            else
            {
                Cup = responseData.Data!;
            }
            return Page();
        }
    }
}
