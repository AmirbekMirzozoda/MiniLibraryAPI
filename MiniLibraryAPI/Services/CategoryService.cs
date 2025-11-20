using MiniLibraryAPI.Data;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    public async Task<Category> AddCategory(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }
    
    
}