
using Microsoft.AspNetCore.Builder;
using WebApplication2.model;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetLoans();
    Task AddLoan(Loan loan);
    Task DeleteLoan(int id);
    Task<IEnumerable<Loan>> GetLoansByBorrowerName(string borrowerName);
    Task<IEnumerable<Loan>> GetLoansByBookId(int bookId);
    
}