using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.BL
{
    public class BooksService
    {
        private readonly LibraryDbContext _context;

        public BooksService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }
        public async Task<bool> UpdateBookAsync(Book book)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.Genre = book.Genre;
            existingBook.ISBN = book.ISBN;

            int saved = await _context.SaveChangesAsync();

            return saved == 1;
        }
        
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}