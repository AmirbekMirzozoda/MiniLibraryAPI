using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Category> AddCategory(Category category);
}