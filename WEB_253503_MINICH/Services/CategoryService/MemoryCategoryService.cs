using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.UI.Services.CategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
                {
                new() {Id=1, Name="Кружки для кофе", NormalizedName="coffee-mugs"},
                new() {Id=2, Name="Кружки для чая", NormalizedName="tea-cups"},
                new() {Id=3, Name="Кружки для пива", NormalizedName="beer-cups"},
                new() {Id=4, Name="Кружки походные", NormalizedName="travel-cups"},
                new() {Id=5, Name="Кружки для детей", NormalizedName="sippy-cups"}
                };
            var result = ResponseData<List<Category>>.Success(categories);
            return Task.FromResult(result);
        }
    }
}
