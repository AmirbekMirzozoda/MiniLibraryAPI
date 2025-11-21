using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    public async Task<Category> AddCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await context.Categories
            .Include(x => x.Books!)
            .ThenInclude(x => x.Author)
            .ToListAsync();
    }
}