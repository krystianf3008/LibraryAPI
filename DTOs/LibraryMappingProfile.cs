using AutoMapper;
using LibraryAPI.Models;
using LibraryAPI.Utilities; 

namespace LibraryAPI.DTOs
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {   
            CreateMap<CreateBookDTO, Book>()
                .ForMember(b => b.Category, cb => cb.MapFrom(c => new Category { Name = c.CategoryName }))
                .ForMember(dest => dest.Base64Cover, opt => opt.MapFrom(src => ImageConverter.ConvertToBase64(src.Cover)));
            CreateMap<UpdateBookDTO, Book>()
                .ForMember(b => b.Category, cb => cb.MapFrom(c => new Category { Name = c.CategoryName }))
                .ForMember(dest => dest.Base64Cover, opt => opt.MapFrom(src => ImageConverter.ConvertToBase64(src.Cover)));
            CreateProjection<Book, BookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name));
            CreateProjection<Book, OneBookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name));
        }
    } 
}
