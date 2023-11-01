using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace LibraryAPI.Entities
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired();
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired();
        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .HasIndex(a => a.FullName)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .Property(a => a.FullName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();
        modelBuilder.Entity<Role>()
            .Property(r => r.Name)
            .IsRequired();
        modelBuilder.Entity<Role>()
        .Property(r => r.Id)
        .ValueGeneratedNever();

        }
    }
}
