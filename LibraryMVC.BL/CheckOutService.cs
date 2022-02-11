using LibraryMVC.Data;
using LibraryMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMVC.BL
{
    public class CheckOutService
    {
        private readonly LibraryDbContext _context;

        public CheckOutService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CheckedOut>> GetAllAsync()
        {
            return await _context.CheckedOuts
                .Include(c => c.Book)
                .Include(c => c.Member)
                .Where(c => c.ReturnedDate == null)
                .ToListAsync();
        }

        public async Task<bool> CheckOutBook(int memberId, int bookId)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            var member = await _context.Members.FirstOrDefaultAsync(c => c.Id == memberId);

            if (book == null || member == null)
            {
                return false;
            }

            book.CheckedOutTo = member;

            var checkOut = new CheckedOut();
            checkOut.Book = book;
            checkOut.Member = member;
            checkOut.CheckedOutDate = DateTime.Now;
            await _context.CheckedOuts.AddAsync(checkOut);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckInBook(int bookId, int memberId)
        {
            var book = await _context.Books.Include(b => b.CheckedOutTo).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return false;
            }

            book.CheckedOutTo = null;

            var checkOut = await _context.CheckedOuts
                .FirstOrDefaultAsync(c => 
                    c.Book.Id == bookId 
                    && c.Member.Id == memberId 
                    && c.ReturnedDate == null);

            if (checkOut == null)
            {
                return false;
            }

            checkOut.ReturnedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
