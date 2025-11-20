namespace MiniLibraryAPI.Entities;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public long AuthorId { get; set; }
    public Author Author { get; set; }
    
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}