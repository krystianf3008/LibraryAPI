using AutoMapper;
using LibraryAPI.Models.Authors;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Categories;
using LibraryAPI.Models.Users;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Utilities;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Models
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
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.Base64Avatar, opt => opt.MapFrom(src => ImageConverter.ConvertToBase64(src.Avatar)));

            CreateProjection<Book, BookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name))
                .ForMember(bd => bd.AuthorFullName, b => b.MapFrom(a => a.Author.FullName));
            CreateProjection<Book, OneBookDTO>()
                .ForMember(bd => bd.CategoryName, b => b.MapFrom(c => c.Category.Name))
                .ForMember(bd => bd.AuthorFullName, b => b.MapFrom(a => a.Author.FullName));
            CreateProjection<User, UserDTO>()
                .ForMember(ud => ud.RoleName, u => u.MapFrom(u => u.Role.Name));
            
        }
    } 
}
