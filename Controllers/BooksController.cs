using Microsoft.AspNetCore.Mvc;
using LMS.Models;
using LMS.Services;
using LMS.Factories;
using System.Collections.Generic;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book book)
        {
            try
            {
                var newBook = BookFactory.CreateBook(book.Id, book.Title, book.Author, book.ISBN);
                _bookService.AddBook(newBook);
                return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/borrow")]
        public IActionResult BorrowBook(int id)
        {
            if (_bookService.BorrowBook(id, out var message))
            {
                return NoContent();
            }
            return BadRequest(message);
        }

        [HttpPut("{id}/return")]
        public IActionResult ReturnBook(int id)
        {
            if (_bookService.ReturnBook(id, out var message))
            {
                return NoContent();
            }
            return BadRequest(message);
        }
    }

}
