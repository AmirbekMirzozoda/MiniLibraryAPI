namespace MiniLibraryAPI.DTOs;

public class ResponseGetList<T>
{
    public T? Data { get; set; }
    
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalRecords { get; set; }
}