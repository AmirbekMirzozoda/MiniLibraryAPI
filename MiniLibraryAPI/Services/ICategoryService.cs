using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(AddCategoryDto categoryDto);
    Task<IEnumerable<Category>> GetCategoriesAsync(CategoryFilter filter);
    Task<Category> UpdateCategoryAsync(CategoryDto category);
    Task DeleteCategoryAsync(long id);
}