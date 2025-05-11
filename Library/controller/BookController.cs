namespace WebApplication2.controller;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.model;
using WebApplication2.repository;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return Ok(await _bookRepository.GetBooks());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookRepository.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author) || book.Quantity < 0)
        {
            return BadRequest("Invalid book data.");
        }

        try
        {
            await _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding book: {ex}");
            return StatusCode(500, "An error occurred while adding the book.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook([FromBody] Book book, int id)
    {
        try
        {
            book.Id = id;
            await _bookRepository.UpdateBook(book);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookRepository.DeleteBook(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> GetBookByFilter([FromQuery] string? title, [FromQuery] string? author)
    {
        var books = await _bookRepository.GetBooks();

        var filteredBooks = books.Where(b =>
            (string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(author) || b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
        ).ToList();

        return Ok(filteredBooks);
    }

}