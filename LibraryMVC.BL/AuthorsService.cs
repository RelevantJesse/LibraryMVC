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
    public class AuthorsService : IService<Author>
    {
        private readonly LibraryDbContext _context;

        public AuthorsService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            Author existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
            if (existingAuthor == null)
            {
                return false;
            }
            existingAuthor.LastName = author.LastName;
            existingAuthor.FirstName = author.FirstName;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            Author author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
