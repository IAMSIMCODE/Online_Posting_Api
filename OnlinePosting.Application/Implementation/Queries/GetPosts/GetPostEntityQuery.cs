using MediatR;
using OnlinePosting.Domain.Models.Dto.Response;

namespace OnlinePosting.Application.Implementation.Queries.GetPosts
{
    public class GetPostEntityQuery : IRequest<List<PostEntityResponseDto>>
    {

    }

   // public record GetPostEntityQuery2 : IRequest<List<PostEntityResponseDto>>;
}
