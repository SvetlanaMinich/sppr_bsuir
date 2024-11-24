using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.API.Data;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.API.Services.CupService
{
    public class CupService : ICupService
    {
        private readonly int _maxPageSize = 3;
        private readonly AppDbContext _appDbContext;

        public CupService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseData<Cup>> CreateCupAsync(Cup cup)
        {
            await _appDbContext.Cups.AddAsync(cup);
            await _appDbContext.SaveChangesAsync();
            return ResponseData<Cup>.Success(cup);
        }

        public async Task DeleteCupAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Cup does not exist.");
            }
            /*await _appDbContext.Cups.Where(m => m.Id == id).ExecuteDeleteAsync();*/
        }

        public async Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            if (id < 0)
            {
                return ResponseData<Cup>.Error("Cup does not exist.");
            }
            var cup = await _appDbContext.Cups.FindAsync(id);
            return ResponseData<Cup>.Success(cup);
        }

        public async Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            var query = _appDbContext.Cups.AsQueryable();
            var dataList = new ProductListModel<Cup>();

            categoryNormalizedName = categoryNormalizedName == "All" ? null : categoryNormalizedName;
            query = query.Where(m => categoryNormalizedName == null || m.Category!.NormalizedName.Equals(categoryNormalizedName));

            //count of elements in list
            var count = await query.CountAsync();
            if (count == 0)
            {
                return ResponseData<ProductListModel<Cup>>.Success(dataList);
            }

            //count of pages
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
            {
                return ResponseData<ProductListModel<Cup>>.Error("No Such Page");
            }

            dataList.Items = await query
                                .Skip((pageNo - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            return ResponseData<ProductListModel<Cup>>.Success(dataList);
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
            {
                return ResponseData<string>.Error("No file uploaded.");
            }

            var imagePath = Path.Combine("wwwroot", "Images");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var fileName = formFile.FileName;
            var filePath = Path.Combine(imagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var url = $"{Path.Combine("Images", fileName)}";
            return ResponseData<string>.Success(url);
        }

        public async Task UpdateCupAsync(int id, Cup cup)
        {
            if (id < 0 || id != cup.Id)
            {
                throw new ArgumentException("Invalid cup Id");
            }

            _appDbContext.Entry(cup).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
