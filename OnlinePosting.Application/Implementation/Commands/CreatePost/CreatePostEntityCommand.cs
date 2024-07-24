using MediatR;
using OnlinePosting.Application.Shared.Dto.Request;
using OnlinePosting.Domain.Models.Dto.Response;

namespace OnlinePosting.Application.Implementation.Commands.CreatePost
{
    public class CreatePostEntityCommand : IRequest<PostEntityResponseDto>
    {
        public PostEntityRequestDto PostRequestDto { get; }

        public CreatePostEntityCommand(PostEntityRequestDto postRequestDto)
        {
            PostRequestDto = postRequestDto;
        }

        //public string Title { get; set; }
        //public string Description { get; set; }
    }
}
