using AutoMapper;
using MediatR;
using OnlinePosting.Domain.Models.Dto.Response;
using OnlinePosting.Domain.Repository;

namespace OnlinePosting.Application.Implementation.Queries.GetSinglePost
{
    public class GetPostEntityByIdHandler : IRequestHandler<GetPostEntityByIdQuery, PostEntityResponseDto>
    {
        private readonly IPostEntityRepository _postRepo;
        private readonly IMapper _mapper;

        public GetPostEntityByIdHandler(IPostEntityRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        public async Task<PostEntityResponseDto> Handle(GetPostEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetByIdAsync(request.PostId);
            if (post == null) { return null; }

            return _mapper.Map<PostEntityResponseDto>(post);
        }
    }
}
