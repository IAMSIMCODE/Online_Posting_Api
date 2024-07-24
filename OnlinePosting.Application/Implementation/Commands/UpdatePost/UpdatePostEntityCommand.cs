using MediatR;
using OnlinePosting.Application.Shared.Dto.Request;

namespace OnlinePosting.Application.Implementation.Commands.UpdatePost
{
    public class UpdatePostEntityCommand(UpdateEntityRequestDto postRequestDto) : IRequest<int>
    {
        public UpdateEntityRequestDto PostRequestDto { get; } = postRequestDto;
    }
}
