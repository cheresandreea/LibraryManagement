using WebApplication2.model;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.repository;

public class LoanRepository : ILoanRepository
{
    private readonly BookDbContext _context;

    public LoanRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetLoans()
    {
        return await _context.Loans.ToListAsync();
    }

    public async Task<Loan> GetLoanById(int id)
    {
        return await _context.Loans.FindAsync(id);
    }

    public async Task AddLoan(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
    }
    

    public async Task DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan != null)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Loan>> GetLoansByBorrowerName(string borrowerName)
    {
        return await _context.Loans.Where(l => l.BorrowerName == borrowerName).ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetLoansByBookId(int bookId)
    {
        return await _context.Loans.Where(l => l.BookIds.Contains(bookId)).ToListAsync();
    }
}