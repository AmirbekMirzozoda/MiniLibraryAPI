using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context, IMapper mapper) : ICategoryService
{
    public async Task<Category> AddCategoryAsync(AddCategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
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

        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id.Value);
        if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(x => x.Description != null && x.Description.Contains(filter.Description));

        return await query.ToListAsync();
    }

    public async Task<Category?> GetByIdWithLinqQueryAsync(long id)
    {
        var query =
            from category in context.Categories
            where category.Id == id
            select category;

        return await query.SingleOrDefaultAsync();
    }

    public async Task<Category?> GetByIdAsync(long id)
    {
        return await context.Categories
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category> UpdateCategoryAsync(CategoryDto category)
    {
        var existingCategory = await GetByIdAsync(category.Id);
        if (existingCategory == null)
        {
            throw new KeyNotFoundException($"Category with Id {category.Id} not found.");
        }
        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        
        context.Categories.Update(existingCategory);
        await context.SaveChangesAsync();
        return existingCategory;
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