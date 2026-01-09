using MiniLibraryAPI.DTOs;

namespace UnitTest.CategoryTests;

public class AddCategoryTests : CategoryServiceTestBase
{
    [Fact]
    public async Task AddCategoryAsync_ShouldAddCategory()
    {
        // Arrange
        var service = CreateService();
        var dto = new AddCategoryDto
        {
            Name = "Fantasy",
            Description = "Fantasy books"
        };

        // Act
        var result = await service.AddCategoryAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Fantasy", result.Name);

        var category = await service.GetByIdAsync(result.Id);
        Assert.NotNull(category);
        Assert.Equal("Fantasy", category.Name);
    }
}