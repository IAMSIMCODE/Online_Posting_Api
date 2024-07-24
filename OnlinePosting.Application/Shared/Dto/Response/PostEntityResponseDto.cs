using OnlinePosting.Application.Shared.AutoMapper;

namespace OnlinePosting.Domain.Models.Dto.Response
{
    public class PostEntityResponseDto : IMappingProfile<PostEntity>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
