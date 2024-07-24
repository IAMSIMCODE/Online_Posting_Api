using AutoMapper;
using MediatR;
using OnlinePosting.Domain.Models.Dto.Response;
using OnlinePosting.Domain.Repository;

namespace OnlinePosting.Application.Implementation.Queries.GetPosts
{
    public class GetPostEntityHandler : IRequestHandler<GetPostEntityQuery, List<PostEntityResponseDto>>
    {
        private readonly IPostEntityRepository _postRepo;
        private readonly IMapper _mapper;

        public GetPostEntityHandler(IPostEntityRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<List<PostEntityResponseDto>> Handle(GetPostEntityQuery request, CancellationToken cancellationToken)
        {
            //throw new InvalidDataException();
            var posts = await _postRepo.GetAllPostsAsync();

            var postsToReturn = _mapper.Map<List<PostEntityResponseDto>>(posts);
            return postsToReturn;

            //You can also do the same thing using linq
            //var postsToReturn = posts.Select(x => new PostEntityResponseDto
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).ToList();
        }
    }
}
 