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
                if (!_context.Author.Any())
                {
                    _context.Author.AddRange(GetAuthors());
                    _context.SaveChanges();
                }
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
                AuthorID = _context.Author.FirstOrDefault(a => a.FullName == "Isaac Asimov").Id,
                PublicationYear = 1951,
                NumberOfPages = 320,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Fantastyka naukowa").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Fundacja i Imperium",
                Description = "Druga część trylogii Fundacja.",
                AuthorID = _context.Author.FirstOrDefault(a => a.FullName == "Isaac Asimov").Id ,
                PublicationYear = 1952,
                NumberOfPages = 288,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Fantastyka naukowa").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Zbrodnia i kara",
                Description = "Powieść kryminalna Fiodora Dostojewskiego.",
                AuthorID = _context.Author.FirstOrDefault(a => a.FullName == "Fiodor Dostojewski").Id,
                PublicationYear = 1866,
                NumberOfPages = 608,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Literatura piękna").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Bracia Karamazow",
                Description = "Ostatnia powieść Fiodora Dostojewskiego.",
                AuthorID = _context.Author.FirstOrDefault(a => a.FullName == "Fiodor Dostojewski").Id,
                PublicationYear = 1880,
                NumberOfPages = 824,
                CategoryID = _context.Category.FirstOrDefault(c => c.Name == "Literatura piękna").Id,
                Base64Cover = "base64_encoded_cover_image"
            },
            new Book
            {
                Title = "Lalka",
                Description = "Jedno z najważniejszych dzieł polskiej literatury.",
                AuthorID = _context.Author.FirstOrDefault(c => c.FullName == "Bolesław Prus").Id,
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

        public List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>
    {
        new Author
        {
            FullName = "Fiodor Dostojewski",
            Country = "Rosja",
            BirthYear = 1821,
            Description = "Rosyjski pisarz i filozof.",
            Base64Photo = "base64_encoded_author_photo"
        },
        new Author
        {
            FullName = "Isaac Asimov",
            Country = "Stany Zjednoczone",
            BirthYear = 1920,
            Description = "Amerykański pisarz i profesor biochemii.",
            Base64Photo = "base64_encoded_author_photo"
        },
        new Author
        {
            FullName = "Bolesław Prus",
            Country = "Polska",
            BirthYear = 1847,
            Description = "Polski pisarz, publicysta i filozof.",
            Base64Photo = "base64_encoded_author_photo"
        }
    };

            return authors;
        }

    }
}
