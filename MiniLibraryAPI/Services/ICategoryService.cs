using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetCategoriesAsync();
}