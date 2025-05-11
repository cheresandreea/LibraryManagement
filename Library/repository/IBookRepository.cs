using WebApplication2.model;

namespace WebApplication2.repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task DeleteBook(int id);
        Task<Book> GetBookById(int id);
    }
}