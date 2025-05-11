namespace WebApplication2.model;

public class Loan
{
    public int Id { get; set; }
    public List<int> BookIds { get; set; }
    public string BorrowerName { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}