using MediatR;
using OnlinePosting.Domain.Repository;

namespace OnlinePosting.Application.Implementation.Commands.DeletePost
{
    public class DeletePostEntityHandler(IPostEntityRepository postRepo) : IRequestHandler<DeletePostEntityCommand, int>
    {
        private readonly IPostEntityRepository _postRepo = postRepo;

        public async Task<int> Handle(DeletePostEntityCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _postRepo.GetByIdAsync(request.Id);
            if (checkExist == null) { return 0; }

            return await _postRepo.DeletePostAsync(request.Id);
        }
    }
}
