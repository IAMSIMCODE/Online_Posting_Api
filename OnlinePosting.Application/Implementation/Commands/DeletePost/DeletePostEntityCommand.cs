using MediatR;

namespace OnlinePosting.Application.Implementation.Commands.DeletePost
{
    public class DeletePostEntityCommand :IRequest<int>
    {
        public int Id { get; set; }
    }
}
