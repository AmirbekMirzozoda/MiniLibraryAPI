using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service, IMemoryCache cache) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] AddCategoryDto categoryDto)
    {
        var createdCategory = await service.AddCategoryAsync(categoryDto);
        return Ok(createdCategory);
    }

    [HttpGet]
    public async Task<ActionResult<Response<ResponseGetList<IEnumerable<Category>>>>> GetCategoriesAsync([FromQuery] CategoryFilter filter)
    {
        var cacheKey = "categories";

        if (!cache.TryGetValue(cacheKey, out Response<ResponseGetList<IEnumerable<Category>>>? categories))
        {
            categories = await service.GetCategoriesAsync(filter);

            cache.Set(
                cacheKey,
                categories,
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(20) // Sliding expiration
                }
                // TimeSpan.FromSeconds(30) // Absolute expiration
            );
        }

        return Ok(categories);
    }
    
    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategoryAsync(CategoryDto category)
    {
        var updatedCategory = await service.UpdateCategoryAsync(category);
        return Ok(updatedCategory);
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteCategoryAsync(long id)
    {
        await service.DeleteCategoryAsync(id);
        return Ok();
    }
}