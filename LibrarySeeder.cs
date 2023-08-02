using LibraryAPI.Entities;
using LibraryAPI.Models;

namespace LibraryAPI
{
    public class LibrarySeeder
    {
        private readonly LibraryDbContext _context;
        public LibrarySeeder(LibraryDbContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if(_context.Database.CanConnect())
            {
                if (!_context.Category.Any())
                {
                    _context.Category.AddRange(GetCategories());
                    _context.SaveChanges();
                }

                if (!_context.Book.Any())
                {
                    _context.Book.AddRange(GetBooks());
                    _context.SaveChanges();
                }
            }
        }
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>
        {
            new Book
            {
                Title = "Fundacja",
                Description = "Pierwsza część trylogii Fundacja.",
                Author = "Isaac Asimov",
                PublicationYear = 1951,
                NumberOfPages = 320,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Fantastyka naukowa").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Fundacja i Imperium",
                Description = "Druga część trylogii Fundacja.",
                Author = "Isaac Asimov",
                PublicationYear = 1952,
                NumberOfPages = 288,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Fantastyka naukowa").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Zbrodnia i kara",
                Description = "Powieść kryminalna Fiodora Dostojewskiego.",
                Author = "Fiodor Dostojewski",
                PublicationYear = 1866,
                NumberOfPages = 608,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Literatura piękna").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Bracia Karamazow",
                Description = "Ostatnia powieść Fiodora Dostojewskiego.",
                Author = "Fiodor Dostojewski",
                PublicationYear = 1880,
                NumberOfPages = 824,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Literatura piękna").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Lalka",
                Description = "Jedno z najważniejszych dzieł polskiej literatury.",
                Author = "Bolesław Prus",
                PublicationYear = 1890,
                NumberOfPages = 632,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Literatura piękna").Id,
                Base64Cover = "base64_encoded_cover_image"
            }
        };

            return books;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>
        {
            new Category { Name = "Fantastyka naukowa" },
            new Category { Name = "Literatura piękna" },
            new Category { Name = "Literatura historyczna" }
            
        };


            return categories;
        }

    }
}
