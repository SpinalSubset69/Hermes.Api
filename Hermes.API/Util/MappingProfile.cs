using AutoMapper;
using Core.entities;
using Hermes.API.Dtos;
using Hermes.API.Util.UrlResolvers;

namespace Hermes.API.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Register Mapping
            CreateMap<RegisterCategory, Category>().ReverseMap();
            CreateMap<RegisterReporter, Reporter>().ReverseMap();
            CreateMap<RegisterArticle, Article>().ReverseMap();
            CreateMap<ImageToReturnDto, Image>().ReverseMap();

            //Return Mapping
            //Reporter Mapping
            CreateMap<Reporter, ReporterToReturnDto>()
                .ForMember(x => x.Rol, s => s.MapFrom(r => r.Role.Name))
                .ForMember(x => x.Image, s => s.MapFrom<ReporterUrlResolver>()).ReverseMap();

            //ArticleMapping
            CreateMap<Article, ArticleToReturnDto>()
                .ForMember(x => x.Category, s => s.MapFrom(r => r.Category.Name));

            CreateMap<Article, ArticleToReturnWithoutReporterDto>()
                .ForMember(x => x.Category, s => s.MapFrom(r => r.Category.Name)).ReverseMap();
        }
    }
}
