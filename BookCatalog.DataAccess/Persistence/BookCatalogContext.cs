using BookCatalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Persistence
{
    public class BookCatalogContext : DbContext
    {
        public BookCatalogContext(DbContextOptions<BookCatalogContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
