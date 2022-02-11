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
    public class MembersService : IService<Member>
    {
        private readonly LibraryDbContext _context;

        public MembersService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return false;
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            Member existingMember = await _context.Members.FirstOrDefaultAsync(m => m.Id == member.Id);
            if (existingMember == null)
            {
                return false;
            }
            existingMember.LastName = member.LastName;
            existingMember.FirstName = member.FirstName;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
