using AdventureWorks.Data.Common;
using AdventureWorks.Data.DTO;
using AdventureWorks.Data.Production.EntityFramework;
using AutoMapper;

namespace AdventureWorks.Web.ServiceInterfaces
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<PagedResult<Product>, PagedResult<ProductDto>>().ReverseMap();
            CreateMap<ProductModel, ProductModelDto>().ReverseMap();
            CreateMap<ProductReview, ProductReviewDto>().ReverseMap();
        }
    }
}