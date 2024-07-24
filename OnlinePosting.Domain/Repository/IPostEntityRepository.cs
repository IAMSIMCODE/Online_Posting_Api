using OnlinePosting.Domain.Models;

namespace OnlinePosting.Domain.Repository
{
    public interface IPostEntityRepository
    {
        Task<List<PostEntity>> GetAllPostsAsync();
        Task<PostEntity> GetByIdAsync(int id);
        Task<PostEntity> CreatePostAsync(PostEntity entity);
        Task<int> UpdatePostAsync(int id, PostEntity entity);
        Task<int> DeletePostAsync(int id);
    }
}
