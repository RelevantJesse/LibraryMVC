using LibraryMVC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMVC.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CheckedOut> CheckedOuts { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Book>()
                .Property(b => b.Genre)
                .HasConversion<int>();
        }
    }
}