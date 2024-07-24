using AutoMapper;
using OnlinePosting.Domain.Models;
using OnlinePosting.Domain.Models.Dto.Response;

namespace OnlinePosting.Application.Shared.AutoMapper
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<PostEntity, PostEntityResponseDto>();
                
                //config.CreateMap<CouponDto, Coupon>().ReverseMap();
                //config.CreateMap<CouponUpdateDto, Coupon>();
            });
            return mappingConfig;
        }
    }
}
