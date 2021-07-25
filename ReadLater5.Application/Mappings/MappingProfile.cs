using AutoMapper;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Models;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bookmark, BookmarkVM>().ReverseMap();
            CreateMap<Bookmark, BookmarkDto>();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryDto>();
        }
    }
}
