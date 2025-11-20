using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
    {
        var createdCategory = await service.AddCategory(category);
        return Ok(createdCategory);
    }
}