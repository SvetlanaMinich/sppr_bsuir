using Microsoft.AspNetCore.Mvc;
using WEB_253503_MINICH.UI.Controllers;
using WEB_253503_MINICH.UI.Services.ApiCupService;
using WEB_253503_MINICH.UI.Services.ApiCategoryService;
using Xunit;
using NSubstitute;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.Domain.Entities;
using Microsoft.AspNetCore.Http;



namespace WEB_253503_MINICH.Tests
{
    public class CupControllerTest
    {
        private readonly ICategoryService _categoryService = Substitute.For<ICategoryService>();
        private readonly ICupService _cupService = Substitute.For<ICupService>();
        private CupController CreateController()
        {
            return new CupController(_cupService, _categoryService);
        }

        [Fact]
        public async Task NotFoundCategories()
        {
            var controller = CreateController();
            _categoryService.GetCategoryListAsync().Returns(
                Task.FromResult(ResponseData<List<Category>>.Error("Error fetching categories")));

            var result = await controller.Index(null);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Error fetching categories", notFoundResult.Value);
        }

        [Fact]
        public async Task NotFoundCup()
        {
            var controller = CreateController();
            _categoryService.GetCategoryListAsync()
                .Returns(ResponseData<List<Category>>.Success(new List<Category>()));
            _cupService.GetCupListAsync("tea-cups", 7).Returns(
                ResponseData<ProductListModel<Cup>>.Error("Cup error"));

            var result = await controller.Index("tea-cups", 7);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Cup error", notFoundResult.Value);
        }

        [Fact]
        public async Task ViewDataReceiveCategories()
        {
            var controller = CreateController();
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            httpContext.Request.Headers["X-Requested-With"] = "";

            var expectedCategories = new List<Category> {
                new Category { Name = "Cat1", NormalizedName = "category-1" },
                new Category { Name = "Cat2", NormalizedName = "category-2" }
            };

            _categoryService.GetCategoryListAsync().Returns(
                Task.FromResult(ResponseData<List<Category>>.Success(expectedCategories)));
            _cupService.GetCupListAsync(null, 1).Returns(
                Task.FromResult(ResponseData<ProductListModel<Cup>>.Success(new ProductListModel<Cup>())));

            var result = await controller.Index(null);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.ViewData["categories"]);
            var categoriesInViewData = viewResult.ViewData["categories"] as List<Category>;
            Assert.Equal(expectedCategories, categoriesInViewData);
        }

        [Fact]
        public async Task CorrectCurrentCategory()
        {
            var controller = CreateController();
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            string cat = "tea-cups";
            var cats = new List<Category>
            {
                new Category { Name="Кружки для кофе", NormalizedName="coffee-mugs" },
                new Category { Name="Кружки для чая", NormalizedName="tea-cups" },
                new Category { Name="Кружки для пива", NormalizedName="beer-cups" },
                new Category { Name="Кружки походные", NormalizedName="travel-cups" },
                new Category { Name="Кружки для детей", NormalizedName="sippy-cups" }
            };

            _categoryService.GetCategoryListAsync().Returns(
                Task.FromResult(ResponseData<List<Category>>.Success(cats)));
            _cupService.GetCupListAsync(cat, 1).Returns(
                Task.FromResult(ResponseData<ProductListModel<Cup>>.Success(new ProductListModel<Cup>())));

            var result = await controller.Index(cat);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Кружки для чая", viewResult.ViewData["currentCategory"]);
        }

        [Fact]
        public async Task CorrectCupListModel()
        {
            var controller = CreateController();
            var httpContext = new DefaultHttpContext();
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            string cat = "tea-cups";
            var cats = new List<Category>
            {
                new Category { Name="Кружки для кофе", NormalizedName="coffee-mugs" },
                new Category { Name="Кружки для чая", NormalizedName="tea-cups" },
                new Category { Name="Кружки для пива", NormalizedName="beer-cups" },
                new Category { Name="Кружки походные", NormalizedName="travel-cups" },
                new Category { Name="Кружки для детей", NormalizedName="sippy-cups" }
            };

            var expectedCups = new ProductListModel<Cup>
            {
                Items = new List<Cup> { new Cup(), new Cup(), new Cup() },
                CurrentPage = 1,
                TotalPages = 2
            };

            _categoryService.GetCategoryListAsync().Returns(
                Task.FromResult(ResponseData<List<Category>>.Success(cats)));
            _cupService.GetCupListAsync(cat, 1).Returns(
                Task.FromResult(ResponseData<ProductListModel<Cup>>.Success(expectedCups)));

            var result = await controller.Index(cat);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductListModel<Cup>>(viewResult.Model);
            Assert.Equal(expectedCups, model);

        }
    }
}
