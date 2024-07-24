using MediatR;
using OnlinePosting.Domain.Models.Dto.Response;

namespace OnlinePosting.Application.Implementation.Queries.GetSinglePost
{
    public class GetPostEntityByIdQuery : IRequest<PostEntityResponseDto>
    {
        public int PostId { get; set; }

        //public GetPostEntityByIdQuery(int postId)
        //{
        //    PostId = postId;
        //}
    }
}
