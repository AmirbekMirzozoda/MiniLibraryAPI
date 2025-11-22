using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetCategoriesAsync(CategoryFilter filter);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(long id);
}