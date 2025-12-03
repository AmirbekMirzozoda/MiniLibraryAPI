using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.DTOs.Filters;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] AddCategoryDto categoryDto)
    {
        var createdCategory = await service.AddCategoryAsync(categoryDto);
        return Ok(createdCategory);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync([FromQuery] CategoryFilter filter)
    {
        var categories = await service.GetCategoriesAsync(filter);
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