using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Entities
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books;
        public DbSet<Category> Categories;
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(b => b.Description)
            .HasMaxLength(1000);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired();
        }
    }
}
