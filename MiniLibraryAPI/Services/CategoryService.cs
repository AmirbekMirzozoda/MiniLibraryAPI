using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context, IMapper mapper, ILogger<CategoryService> logger) : ICategoryService
{
    public async Task<Category> AddCategoryAsync(AddCategoryDto categoryDto)
    {
        try
        {
            var category = mapper.Map<Category>(categoryDto);
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            logger.LogInformation("Category added! Id: {CategoryId}", category.Id);
            return category;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error adding category");
            return new Category();
        }
    }

    public async Task<Response<ResponseGetList<IEnumerable<Category>>>?> GetCategoriesAsync(CategoryFilter f)
    {
        var query = context.Categories
            .Include(x => x.Books!)
            .ThenInclude(x => x.Author)
            .AsQueryable();

        if (f.Id.HasValue) query = query.Where(x => x.Id == f.Id.Value);
        if (!string.IsNullOrEmpty(f.Name)) query = query.Where(x => x.Name.Contains(f.Name));
        if (!string.IsNullOrEmpty(f.Description))
            query = query.Where(x => x.Description != null && x.Description.Contains(f.Description));

        return new Response<ResponseGetList<IEnumerable<Category>>>
        {
            Code = (int)HttpStatusCode.OK,
            Message = "Success",
            Payload = new ResponseGetList<IEnumerable<Category>>
            {
                Data = await query
                    .Skip((f.Page-1) * f.Size)
                    .Take(f.Size)
                    .ToListAsync(),
                Page = f.Page,
                Size = f.Size,
                TotalRecords = await query.CountAsync()
            }
        };
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