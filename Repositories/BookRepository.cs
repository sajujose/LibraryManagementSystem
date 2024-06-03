using LMS.Models;
using System.Collections.Generic;

namespace LMS.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
    }

    public class BookRepository : IBookRepository
    {
        private readonly Library _library;

        public BookRepository()
        {
            _library = Library.Instance;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _library.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return _library.GetBookById(id);
        }

        public void AddBook(Book book)
        {
            _library.AddBook(book);
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _library.GetBookById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.IsBorrowed = book.IsBorrowed;
            }
        }
    }
}
