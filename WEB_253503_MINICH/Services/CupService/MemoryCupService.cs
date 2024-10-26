using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.UI.Services.CategoryService;

namespace WEB_253503_MINICH.UI.Services.CupService
{
    public class MemoryCupService : ICupService
    {
        private readonly IConfiguration _config;
        List<Cup> _cups;
        List<Category> _categories;
        public MemoryCupService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync().Result.Data!;
            SetupData();
        }
        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _cups = new List<Cup>
             {
                new Cup
                {
                    Id = 1,
                    Name = "Новогодняя кружка",
                    Description = "230 мл, фарфор, белый-красный",
                    Price = 200,
                    ImgPath = "Images/cup1.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("coffee-mugs"))
                },
                new Cup
                {
                    Id = 2,
                    Name = "Золотая кофейная кружка",
                    Description = "350 мл, керамика, золотая отделка",
                    Price = 350,
                    ImgPath = "Images/cup2.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("coffee-mugs"))
                },
                new Cup
                {
                    Id = 3,
                    Name = "Чайная кружка в цветочек",
                    Description = "300 мл, фарфор, бело-голубая",
                    Price = 180,
                    ImgPath = "Images/cup3.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("tea-cups"))
                },
                new Cup
                {
                    Id = 4,
                    Name = "Зеленая чайная кружка",
                    Description = "400 мл, стекло, зеленый",
                    Price = 220,
                    ImgPath = "Images/Cup4.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("tea-cups"))
                },
                new Cup
                {
                    Id = 5,
                    Name = "Пивная кружка XL",
                    Description = "500 мл, стекло, прозрачная",
                    Price = 300,
                    ImgPath = "Images/Cup5.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("beer-cups"))
                },
                new Cup
                {
                    Id = 6,
                    Name = "Традиционная пивная кружка",
                    Description = "600 мл, керамика, темно-коричневая",
                    Price = 450,
                    ImgPath = "Images/Cup6.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("beer-cups"))
                },
                new Cup
                {
                    Id = 7,
                    Name = "Походная кружка из нержавеющей стали",
                    Description = "300 мл, нержавеющая сталь, с крышкой",
                    Price = 500,
                    ImgPath = "Images/Cup7.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("travel-cups"))
                },
                new Cup
                {
                    Id = 8,
                    Name = "Походная термокружка",
                    Description = "400 мл, нержавеющая сталь, вакуумная",
                    Price = 600,
                    ImgPath = "Images/Cup8.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("travel-cups"))
                },
                new Cup
                {
                    Id = 9,
                    Name = "Детская кружка с ручками",
                    Description = "250 мл, пластик, зеленая с животными",
                    Price = 150,
                    ImgPath = "Images/Cup9.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("sippy-cups"))
                },
                new Cup
                {
                    Id = 10,
                    Name = "Детская кружка с поилкой",
                    Description = "200 мл, пластик, красная с крышкой",
                    Price = 130,
                    ImgPath = "Images/Cup10.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("sippy-cups"))
                },
                new Cup
                {
                    Id = 11,
                    Name = "Лимитированная кофейная кружка",
                    Description = "330 мл, фарфор, с праздничным рисунком",
                    Price = 320,
                    ImgPath = "Images/Cup11.jpeg",
                    MimeImgType = "image/jpeg",
                    Category = _categories.Find(c => c.NormalizedName.Equals("coffee-mugs"))
                }
            };
        }

        public Task<ResponseData<Cup>> CreateCupAsync(Cup cup, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCupAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, [FromRoute]int page)
        {
            var itemsPerPage = Convert.ToInt32(_config.GetRequiredSection("ItemsPerPage").Value);
            var filteredCups = _cups
                 .Where(d => categoryNormalizedName == null ||
                 d.Category!.NormalizedName.Equals(categoryNormalizedName))
                .ToList();
            var totalCups = filteredCups.Count;
            var totalPages = (int)Math.Ceiling(totalCups / (double)itemsPerPage);
            var cupsOnPage = filteredCups
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
            var productListModel = new ProductListModel<Cup>
            {
                Items = cupsOnPage,
                CurrentPage = page,
                TotalPages = totalPages,
            };
            var result = ResponseData<ProductListModel<Cup>>.Success(productListModel);
            return Task.FromResult(result);
        }

        public Task UpdateCupAsync(int id, Cup cup, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
