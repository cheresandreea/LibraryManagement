using WebApplication2.model;

using WebApplication2.Data;
namespace WebApplication2.repository;
using System.Collections.Generic;
using WebApplication2.model;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly BookDbContext _context;
    public BookRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }
    
    public async Task<Book> GetBookById(int id)
    {
        return await _context.Books.FindAsync(id);
    }
    
    public async Task AddBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateBook(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
    
    
}