using Microsoft.EntityFrameworkCore;
using WEB_253503_MINICH.API.Data;
using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.API.Services.CupService
{
    public class CupService : ICupService
    {
        private readonly int _maxPageSize = 20;
        private readonly AppDbContext _appDbContext;

        public CupService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ResponseData<int>> CreateCupAsync(Cup cup)
        {
            var new_cup = await _appDbContext.Cups.AddAsync(cup);
            await _appDbContext.SaveChangesAsync();
            return ResponseData<int>.Success(new_cup.Entity.Id);
        }

        public async Task<ResponseData<bool>> DeleteCupAsync(int id)
        {
            if (id < 0)
            {
                return ResponseData<bool>.Error("Cup does not exist.");
            }
            await _appDbContext.Cups.Where(m => m.Id == id).ExecuteDeleteAsync();
            return ResponseData<bool>.Success(true);
        }

        public async Task<ResponseData<Cup>> GetCupByIdAsync(int id)
        {
            if (id < 0)
            {
                return ResponseData<Cup>.Error("Cup does not exist.");
            }
            var cup = await _appDbContext.Cups
                .AsNoTracking()
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return ResponseData<Cup>.Success(cup!);
        }

        public async Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }

            var query = _appDbContext.Cups.Include(m => m.Category).AsQueryable();
            var dataList = new ProductListModel<Cup>();

            categoryNormalizedName = categoryNormalizedName == "all" ? null : categoryNormalizedName;
            query = query.Where(m => categoryNormalizedName == null || m.Category.NormalizedName.Equals(categoryNormalizedName));

            //count of elements in list
            var itemsCount = await query.CountAsync();
            if (itemsCount == 0)
            {
                return ResponseData<ProductListModel<Cup>>.Success(dataList);
            }

            //count of pages
            int totalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);

            if (pageNo > totalPages)
            {
                return ResponseData<ProductListModel<Cup>>.Error("No Such Page");
            }

            dataList.Items = await query
                .OrderBy(m => m.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            return ResponseData<ProductListModel<Cup>>.Success(dataList);
        }

        public async Task<ResponseData<bool>> UpdateCupAsync(int id, Cup cup)
        {
            if (id < 0 || id != cup.Id)
            {
                return ResponseData<bool>.Error("Invalid cup Id");
            }

            var selectedCup = await _appDbContext.Cups.FirstOrDefaultAsync(m => m.Id == id);

            if (selectedCup == null)
            {
                return ResponseData<bool>.Error("Cup not found");
            }

            if (cup.ImgPath == null)
            {
                cup.ImgPath = selectedCup.ImgPath;
            }

            selectedCup.Name = cup.Name;
            selectedCup.Description = cup.Description;
            selectedCup.Price = cup.Price;
            selectedCup.ImgPath = cup.ImgPath;

            if (cup.Category != null)
            {
                var category = await _appDbContext.Categories.FindAsync(cup.Category.Id);
                if (category != null)
                {
                    selectedCup.Category = category;
                }
            }

            await _appDbContext.SaveChangesAsync();

            return ResponseData<bool>.Success(true);
        }
    }
}
