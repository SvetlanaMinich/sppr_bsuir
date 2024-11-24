using WEB_253503_MINICH.API.Services.CupService;
using WEB_253503_MINICH.API.Services.CategoryService;
using WEB_253503_MINICH.Domain.Models;
using WEB_253503_MINICH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.API.Data;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Reflection.Metadata;
using NSubstitute.Core;


namespace WEB_253503_MINICH.Tests
{
    public class CupServiceTest
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<AppDbContext> _contextOptions;
        public CupServiceTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;

            // Create the schema and seed some data
            using var context = new AppDbContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
                            CREATE VIEW AllResources AS
                            SELECT Name
                            FROM Categories;";
                viewCommand.ExecuteNonQuery();
            }
            var categories = new List<Category>
            {
                new Category { Name="Кружки для кофе", NormalizedName="coffee-mugs" },
                new Category { Name="Кружки для чая", NormalizedName="tea-cups" },
            };

            context.AddRange(categories);

            var url = "Images/";
            var coffeeMugs = categories.First(c => c.NormalizedName == "coffee-mugs");
            var teaCups = categories.First(c => c.NormalizedName == "tea-cups");

            var products = new List<Cup>
            {
                new Cup { Name = "Новогодняя кружка", Description = "230 мл, фарфор, белый-красный", Price = 200, ImgPath = url + "cup1.jpeg", MimeImgType = "image/jpeg", Category = coffeeMugs },
                new Cup { Name = "Золотая кофейная кружка", Description = "350 мл, керамика, золотая отделка", Price = 350, ImgPath = url + "cup2.jpeg", MimeImgType = "image/jpeg", Category = coffeeMugs },
                new Cup { Name = "Чайная кружка в цветочек", Description = "300 мл, фарфор, бело-голубая", Price = 180, ImgPath = url + "cup3.jpeg", MimeImgType = "image/jpeg", Category = teaCups },
                new Cup { Name = "Зеленая чайная кружка", Description = "400 мл, стекло, зеленый", Price = 220, ImgPath = url + "Cup4.jpeg", MimeImgType = "image/jpeg", Category = teaCups },
            };

            context.AddRange(products);

            context.SaveChanges();
        }

        private AppDbContext CreateContext() => new AppDbContext(_contextOptions);
        private void Dispose() => _connection.Dispose();

        [Fact]
        public void ServiceReturnsFirstPageOfThreeItems()
        {
            using var context = CreateContext();
            var service = new CupService(context);
            var result = service.GetCupListAsync("All").Result;
            Assert.IsType<ResponseData<ProductListModel<Cup>>>(result);
            Assert.True(result.Successfull);
            Assert.Equal(1, result.Data.CurrentPage);
            Assert.Equal(3, result.Data.Items.Count);
            Assert.Equal(2, result.Data.TotalPages);
            Assert.Equal(context.Cups.First(), result.Data.Items[0]);
        }
    }
}
