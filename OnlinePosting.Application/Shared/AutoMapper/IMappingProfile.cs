using AutoMapper;

namespace OnlinePosting.Application.Shared.AutoMapper
{
    public interface IMappingProfile<T> where T : class
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T),GetType());
    }


}
