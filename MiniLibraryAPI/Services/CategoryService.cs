using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs.Filters;
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

    public async Task<IEnumerable<Category>> GetCategoriesAsync(CategoryFilter filter)
    {
        var query = context.Categories
            .Include(x => x.Books!)
            .ThenInclude(x => x.Author)
            .AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description != null && x.Description.Contains(filter.Description));
        }
            
        return await query.ToListAsync();
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(long id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}