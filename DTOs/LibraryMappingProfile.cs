using AutoMapper;
using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {
            CreateMap<CreateBookDTO, Book>()
                .ForMember(b => b.Category, cb => cb.MapFrom(c => new Category { Name = c.CategoryName }));
            CreateProjection<Book, BookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name));
            CreateProjection<Book, OneBookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name));
        }
    } 
}
