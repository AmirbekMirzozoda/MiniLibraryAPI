using System.ComponentModel.DataAnnotations;

namespace MiniLibraryAPI.Entities;

public class BaseEntity
{
    [Key] public long Id { get; set; }
}