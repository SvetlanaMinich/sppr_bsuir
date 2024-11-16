using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
/*using WEB_253503_MINICH.UI.Data;*/
using WEB_253503_MINICH.UI.Services.ApiCupService;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICupService _cupService;

        public IndexModel(ICupService cupService)
        {
            _cupService = cupService;
        }
        [BindProperty]
        public ResponseData<ProductListModel<Cup>> Cup { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Cup = await _cupService.GetCupListAsync(null, 0);
        }
    }
}
