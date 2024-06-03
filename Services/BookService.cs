using LMS.Models;
using LMS.Repositories;
using System.Collections.Generic;

namespace LMS.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int bookId);
        void AddBook(Book book);
        bool BorrowBook(int bookId, out string message);
        bool ReturnBook(int bookId, out string message);
    }

    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _repository.GetAllBooks();
        }

        public Book GetBookById(int bookId)
        {
            return _repository.GetBookById(bookId);
        }

        public void AddBook(Book book)
        {
            if (book == null || string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            {
                throw new ArgumentException("Invalid book details.");
            }
            if (_repository.GetBookById(book.Id) != null)
            {
                throw new ArgumentException("Book with this ID already exists.");
            }
            _repository.AddBook(book);
        }

        public bool BorrowBook(int bookId, out string message)
        {
            var book = _repository.GetBookById(bookId);
            if (book == null)
            {
                message = "Book not found.";
                return false;
            }

            if (book.IsBorrowed)
            {
                message = "Book is already borrowed.";
                return false;
            }

            book.IsBorrowed = true;
            _repository.UpdateBook(book);
            message = "Book borrowed successfully.";
            return true;
        }

        public bool ReturnBook(int bookId, out string message)
        {
            var book = _repository.GetBookById(bookId);
            if (book == null)
            {
                message = "Book not found.";
                return false;
            }

            if (!book.IsBorrowed)
            {
                message = "Book is not currently borrowed.";
                return false;
            }

            book.IsBorrowed = false;
            _repository.UpdateBook(book);
            message = "Book returned successfully.";
            return true;
        }
    }
}
