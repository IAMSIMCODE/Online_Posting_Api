using MediatR;
using OnlinePosting.Domain.Models;
using OnlinePosting.Domain.Repository;

namespace OnlinePosting.Application.Implementation.Commands.UpdatePost
{
    public class UpdatePostEntityHandler(IPostEntityRepository postRepo) : IRequestHandler<UpdatePostEntityCommand, int>
    {
        private readonly IPostEntityRepository _postRepo = postRepo;

        public async Task<int> Handle(UpdatePostEntityCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _postRepo.GetByIdAsync(request.PostRequestDto.Id);
            if (checkExist == null) { return 0; }

            var postToUpdate = new PostEntity()
            {
                Id = request.PostRequestDto.Id,
                Title = request.PostRequestDto.Title,
                Description = request.PostRequestDto.Description
            };

            return await _postRepo.UpdatePostAsync(request.PostRequestDto.Id, postToUpdate);
        }
    }
}
