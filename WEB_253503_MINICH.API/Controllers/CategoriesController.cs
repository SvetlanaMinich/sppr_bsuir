using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.API.Services.CategoryService;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IQueryable<Category>>> GetCategories()
        {
            var response = await _categoryService.GetCategoryListAsync();
            return new ActionResult<IQueryable<Category>>(response.Data!.AsQueryable());
        }
    }
}
