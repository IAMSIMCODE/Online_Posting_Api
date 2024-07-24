 using AutoMapper;
using MediatR;
using OnlinePosting.Domain.Models;
using OnlinePosting.Domain.Models.Dto.Response;
using OnlinePosting.Domain.Repository;

namespace OnlinePosting.Application.Implementation.Commands.CreatePost
{
    public class CreatePostEntityHandler : IRequestHandler<CreatePostEntityCommand, PostEntityResponseDto>
    {
        private readonly IPostEntityRepository _postRepo;
        private readonly IMapper _mapper;

        public CreatePostEntityHandler(IPostEntityRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostEntityResponseDto> Handle(CreatePostEntityCommand request, CancellationToken cancellationToken)
        {
            var postToCreate = new PostEntity()
            {
                Title = request.PostRequestDto.Title,
                Description = request.PostRequestDto.Description,
            };

            var createPost = await _postRepo.CreatePostAsync(postToCreate);
            return _mapper.Map<PostEntityResponseDto>(createPost);
        }
    }
}
