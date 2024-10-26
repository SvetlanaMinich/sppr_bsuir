using WEB_253503_MINICH.Domain.Entities;
using WEB_253503_MINICH.Domain.Models;

namespace WEB_253503_MINICH.API.Services.CupService
{
    public interface ICupService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории для фильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <returns></returns>
        public Task<ResponseData<ProductListModel<Cup>>> GetCupListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize=3);

        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Найденный объект или null, если объект не найден</returns>
        public Task<ResponseData<Cup>> GetCupByIdAsync(int id);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемомго объекта</param>
        /// <param name="cup">объект с новыми параметрами</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns></returns>
        public Task<ResponseData<bool>> UpdateCupAsync(int id, Cup cup);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемомго объекта</param>
        /// <returns></returns>
        public Task<ResponseData<bool>> DeleteCupAsync(int id);

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="cup">Новый объект</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<int>> CreateCupAsync(Cup cup);

        /// <summary> 
        /// Сохранить файл изображения для объекта 
        /// </summary> 
        /// <param name="id">Id объекта</param> 
        /// <param name="formFile">файл изображения</param> 
        /// <returns>Url к файлу изображения</returns 
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}

