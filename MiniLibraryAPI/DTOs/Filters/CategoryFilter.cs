namespace MiniLibraryAPI.DTOs.Filters;

public class CategoryFilter
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}