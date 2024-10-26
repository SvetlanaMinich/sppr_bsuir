using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.Domain.Entities;

namespace WEB_253503_MINICH.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // await context.Database.MigrateAsync();

            //delete data
            /*context.Cups.RemoveRange(context.Cups);
            context.Categories.RemoveRange(context.Categories);
            await context.Database.ExecuteSqlRawAsync("DELETE FROM cups;");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM categories;");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'cups';");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'categories';");
            await context.SaveChangesAsync();*/

            // Проверяем, пустая ли база данных, чтобы не дублировать записи
            if (context.Cups.Any() || context.Categories.Any()) return;

            // Создаем категории
            var categories = new List<Category>
            {
                new Category { Name="Кружки для кофе", NormalizedName="coffee-mugs" },
                new Category { Name="Кружки для чая", NormalizedName="tea-cups" },
                new Category { Name="Кружки для пива", NormalizedName="beer-cups" },
                new Category { Name="Кружки походные", NormalizedName="travel-cups" },
                new Category { Name="Кружки для детей", NormalizedName="sippy-cups" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Получаем добавленные категории из базы
            var coffeeMugs = categories.First(c => c.NormalizedName == "coffee-mugs");
            var teaCups = categories.First(c => c.NormalizedName == "tea-cups");
            var beerCups = categories.First(c => c.NormalizedName == "beer-cups");
            var travelCups = categories.First(c => c.NormalizedName == "travel-cups");
            var sippyCups = categories.First(c => c.NormalizedName == "sippy-cups");

            var url = app.Configuration.GetSection("ImagesUrl").Value;

            // Создаем список кружек
            var cups = new List<Cup>
            {
                new Cup { Name = "Новогодняя кружка", Description = "230 мл, фарфор, белый-красный", Price = 200, ImgPath = url + "cup1.jpeg", MimeImgType = "image/jpeg", Category = coffeeMugs },
                new Cup { Name = "Золотая кофейная кружка", Description = "350 мл, керамика, золотая отделка", Price = 350, ImgPath = url + "cup2.jpeg", MimeImgType = "image/jpeg", Category = coffeeMugs },
                new Cup { Name = "Чайная кружка в цветочек", Description = "300 мл, фарфор, бело-голубая", Price = 180, ImgPath = url + "cup3.jpeg", MimeImgType = "image/jpeg", Category = teaCups },
                new Cup { Name = "Зеленая чайная кружка", Description = "400 мл, стекло, зеленый", Price = 220, ImgPath = url + "Cup4.jpeg", MimeImgType = "image/jpeg", Category = teaCups },
                new Cup { Name = "Пивная кружка XL", Description = "500 мл, стекло, прозрачная", Price = 300, ImgPath = url + "Cup5.jpeg", MimeImgType = "image/jpeg", Category = beerCups },
                new Cup { Name = "Традиционная пивная кружка", Description = "600 мл, керамика, темно-коричневая", Price = 450, ImgPath = url + "Cup6.jpeg", MimeImgType = "image/jpeg", Category = beerCups },
                new Cup { Name = "Походная кружка из нержавеющей стали", Description = "300 мл, нержавеющая сталь, с крышкой", Price = 500, ImgPath = url + "Cup7.jpeg", MimeImgType = "image/jpeg", Category = travelCups },
                new Cup { Name = "Походная термокружка", Description = "400 мл, нержавеющая сталь, вакуумная", Price = 600, ImgPath = url + "Cup8.jpeg", MimeImgType = "image/jpeg", Category = travelCups },
                new Cup { Name = "Детская кружка с ручками", Description = "250 мл, пластик, зеленая с животными", Price = 150, ImgPath = url + "Cup9.jpeg", MimeImgType = "image/jpeg", Category = sippyCups },
                new Cup { Name = "Детская кружка с поилкой", Description = "200 мл, пластик, красная с крышкой", Price = 130, ImgPath = url + "Cup10.jpeg", MimeImgType = "image/jpeg", Category = sippyCups },
                new Cup { Name = "Лимитированная кофейная кружка", Description = "330 мл, фарфор, с праздничным рисунком", Price = 320, ImgPath = url + "Cup11.jpeg", MimeImgType = "image/jpeg", Category = coffeeMugs }
            };

            await context.Cups.AddRangeAsync(cups);
            await context.SaveChangesAsync();

            /*// Сохранение изображений в папку wwwroot/Images
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            foreach (var cup in cups)
            {
                var sourcePath = Path.Combine("SeedDataImages", Path.GetFileName(cup.ImgPath)); // Исходные изображения
                var destinationPath = Path.Combine(imagesFolder, Path.GetFileName(cup.ImgPath));
                if (File.Exists(sourcePath) && !File.Exists(destinationPath))
                {
                    File.Copy(sourcePath, destinationPath);
                }
            }*/
        }
    }
}
