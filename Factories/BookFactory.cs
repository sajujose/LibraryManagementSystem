using LMS.Models;

namespace LMS.Factories
{
    public static class BookFactory
    {
        public static Book CreateBook(int id, string title, string author, string isbn)
        {
            return new Book
            {
                Id =id,
                Title = title,
                Author = author,
                ISBN = isbn,
                IsBorrowed = false
            };
        }
    }
}
