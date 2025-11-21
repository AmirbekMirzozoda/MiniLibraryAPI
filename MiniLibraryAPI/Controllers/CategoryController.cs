using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] Category category)
    {
        var createdCategory = await service.AddCategoryAsync(category);
        return Ok(createdCategory);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
    {
        var categories = await service.GetCategoriesAsync();
        return Ok(categories);
    }
}