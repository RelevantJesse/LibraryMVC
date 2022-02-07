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

            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.Author.Id);

            if (author == null)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = author;
            existingBook.Year = book.Year;
            existingBook.Genre = book.Genre;
            existingBook.ISBN = book.ISBN;

            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(a => a.Id == id) ?? new Book();
        }

        public async Task<bool> DeleteBookById(int id)
        {
            var book = await GetBookByIdAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}