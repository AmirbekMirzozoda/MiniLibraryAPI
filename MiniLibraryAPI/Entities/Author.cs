using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniLibraryAPI.Entities;

[Table("BookAuthors")]
public class Author : BaseEntity
{
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(60)]
    public string LastName { get; set; }
    
    [NotMapped] 
    public string FullName => $"{FirstName} {LastName}";
    
    public ICollection<Book>? Books { get; set; }
}