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
    public class AuthorsService
    {
        private readonly LibraryDbContext _context;

        public AuthorsService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
