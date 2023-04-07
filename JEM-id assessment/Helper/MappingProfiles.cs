using AutoMapper;
using JEM_id_assessment.Dto;
using JEM_id_assessment.Models;

namespace JEM_id_assessment.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Article, ArticleDto>();
            CreateMap<ArticleDto, Article>();
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<ProductGroupDto, ProductGroup>();
        }
    }
}
