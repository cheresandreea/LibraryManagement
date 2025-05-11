namespace WebApplication2.model;
using System.ComponentModel.DataAnnotations;


public class Loan
{
    
    public int Id { get; set; }
    [Required]
    public List<int> BookIds { get; set; }
    [Required]
    public string BorrowerName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}