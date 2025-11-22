using System.ComponentModel.DataAnnotations.Schema;

namespace MiniLibraryAPI.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    [NotMapped] 
    public string FullName => $"{FirstName} {LastName}";
    
    public ICollection<Book>? Books { get; set; }
}