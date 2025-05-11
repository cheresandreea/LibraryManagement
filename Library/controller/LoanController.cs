using Microsoft.AspNetCore.Mvc;
using WebApplication2.model;
using WebApplication2.repository;
using WebApplication2.service;

namespace WebApplication2.controller;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IEmailService _emailService;

    public LoanController(ILoanRepository loanRepository, IBookRepository bookRepository, IEmailService emailService)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _emailService = emailService;
    }

    [HttpPost("loan")]
    public async Task<IActionResult> LoanBooks([FromBody] List<int> bookIds, string borrowerName)
    {
        if (string.IsNullOrEmpty(borrowerName))
        {
            return BadRequest("Borrower name is required.");
        }

        if (bookIds == null || bookIds.Count == 0)
        {
            return BadRequest("No book IDs provided.");
        }

        var books = new List<Book>();
        foreach (var bookId in bookIds)
        {
            var book = await _bookRepository.GetBookById(bookId);
            if (book == null || book.Quantity <= 0)
            {
                return BadRequest($"Book with ID {bookId} is unavailable.");
            }
            books.Add(book);
        }

        var loan = new Loan
        {
            BookIds = bookIds,
            BorrowerName = borrowerName,
            LoanDate = DateTime.Now,
            ReturnDate = null
        };

        await _loanRepository.AddLoan(loan);

        foreach (var book in books)
        {
            book.Quantity--;
            await _bookRepository.UpdateBook(book);
        }

        return Ok("Books loaned successfully.");
    }
    

    [HttpPost("return")]
    public async Task<IActionResult> ReturnBooks([FromBody] string borrowerName)
    {
        if (string.IsNullOrEmpty(borrowerName))
        {
            return BadRequest("Borrower name is required.");
        }

        var loan = (await _loanRepository.GetLoansByBorrowerName(borrowerName)).FirstOrDefault();
        if (loan == null)
        {
            return BadRequest("No active loan found for these books.");
        }

        foreach (var bookId in loan.BookIds)
        {
            var book = await _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                return BadRequest($"Book with ID {bookId} not found.");
            }
            book.Quantity++;
            await _bookRepository.UpdateBook(book);
        }

        loan.ReturnDate = DateTime.Now;
        await _loanRepository.DeleteLoan(loan.Id);

        return Ok("Books returned successfully.");
    }
    
    [HttpGet("loans")]
    public async Task<IActionResult> GetLoans()
    {
        var loans = await _loanRepository.GetLoans();
        return Ok(loans);
    }
    
    [HttpPost("send-overdue-reminders")]
    public async Task<IActionResult> SendOverdueReminders()
    {
        var allLoans = await _loanRepository.GetLoans();
        var overdueLoans = allLoans.Where(loan =>
                loan.ReturnDate == null && loan.LoanDate.AddSeconds(10) < DateTime.Now
        ).ToList();

        foreach (var loan in overdueLoans)
        {
            var bookTitles = new List<string>();
            foreach (var bookId in loan.BookIds)
            {
                var book = await _bookRepository.GetBookById(bookId);
                if (book != null)
                {
                    bookTitles.Add(book.Title);
                }
            }

            var subject = "Reminder: Overdue Library Book(s)";
            var body = $"Dear {loan.BorrowerName},\n\nYou have the following book(s) overdue:\n\n" +
                       string.Join("\n", bookTitles) +
                       $"\n\nPlease return them to the library as soon as possible.\n\nThank you,\nThe Library Team";

            try
            {
                await _emailService.SendEmailAsync(loan.BorrowerName, subject, body);
                Console.WriteLine($"Sent overdue reminder to {loan.BorrowerName} for books: {string.Join(", ", bookTitles)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email to {loan.BorrowerName}: {ex.Message}");
            }
        }

        return Ok("Overdue reminders sent.");
    }
}