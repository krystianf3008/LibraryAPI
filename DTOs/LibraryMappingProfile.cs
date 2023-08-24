using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Utilities; 

namespace LibraryAPI.DTOs
{
    public class LibraryMappingProfile : Profile
    {
        public LibraryMappingProfile()
        {   
            CreateMap<CreateBookDTO, Book>()
                .ForMember(dest => dest.Base64Cover, opt => opt.MapFrom(src => ImageConverter.ConvertToBase64(src.Cover)));
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(dest => dest.Base64Photo, opt => opt.MapFrom(src => ImageConverter.ConvertToBase64(src.Photo)));
            CreateMap<CreateCategoryDTO, Category>();
            
            
            CreateProjection<Book, BookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name))
                .ForMember(bd => bd.AuthorFullName, b => b.MapFrom(a => a.Author.FullName));
            CreateProjection<Book, OneBookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name))
                .ForMember(bd => bd.AuthorFullName, b => b.MapFrom(a => a.Author.FullName));
            
        }
    } 
}
