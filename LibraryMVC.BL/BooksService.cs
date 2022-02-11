using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.BL
{
    public class BooksService : IService<Book>
    {
        private readonly LibraryDbContext _context;

        public BooksService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Book book)
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
        
        public async Task<bool> AddAsync(Book book)
        {
            if (book == null)
                return false;

            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.Author.Id);

            if (author == null)
                return false;

            book.Author = author;
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(a => a.Id == id) ?? new Book();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var book = await GetByIdAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}