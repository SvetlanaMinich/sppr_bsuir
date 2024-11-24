using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using WEB_253503_MINICH.API.Data;*/
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.UI.Areas.Admin.Pages.Cups
{
    public class IndexModel : PageModel
    {
        private readonly ICupService _CupService;

        public IndexModel(ICupService CupService)
        {
            _CupService = CupService;
        }
        [BindProperty]
        public ResponseData<ProductListModel<Cup>> Cup { get; set; } = default!;

        public async Task OnGetAsync(int pageNo = 1)
        {
            Cup = await _CupService.GetCupListAsync(null, pageNo);
            //var responseData = await _movieService.GetMovieListAsync("all", pageNo);
            //Movie = responseData.Data.Items;
        }
    }
}