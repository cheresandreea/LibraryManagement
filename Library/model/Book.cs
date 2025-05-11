namespace WebApplication2.model;
using System.ComponentModel.DataAnnotations;

public class Book
{
    
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}