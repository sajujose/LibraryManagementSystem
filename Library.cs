using LMS.Models;
using System.Collections.Generic;
using System.Linq;

namespace LMS
{
    public class Library
    {
        private static readonly Library _instance = new Library();
        private List<Book> _books;

        private Library()
        {
            _books = new List<Book>();
        }

        public static Library Instance => _instance;

        public IEnumerable<Book> GetAllBooks()
        {
            return _books;
        }

        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(book => book.Id == id);
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public bool BorrowBook(int id)
        {
            var book = GetBookById(id);
            if (book != null && !book.IsBorrowed)
            {
                book.IsBorrowed = true;
                return true;
            }
            return false;
        }

        public bool ReturnBook(int id)
        {
            var book = GetBookById(id);
            if (book != null && book.IsBorrowed)
            {
                book.IsBorrowed = false;
                return true;
            }
            return false;
        }
    }
}
