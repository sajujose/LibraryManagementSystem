using Microsoft.EntityFrameworkCore;
using LMS.Models;
using System.Collections.Generic;

namespace LMS.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
