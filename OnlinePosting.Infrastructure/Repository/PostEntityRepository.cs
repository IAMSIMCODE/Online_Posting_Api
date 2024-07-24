using Microsoft.EntityFrameworkCore;
using OnlinePosting.Domain.Models;
using OnlinePosting.Domain.Repository;
using OnlinePosting.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePosting.Infrastructure.Repository
{
    public class PostEntityRepository(OnlinePostDbContext dbContext) : IPostEntityRepository
    {
        private readonly OnlinePostDbContext _dbContext = dbContext;

        public async Task<PostEntity> CreatePostAsync(PostEntity entity)
        {
            await _dbContext.PostEntities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();    

            return entity;
        }

        public async Task<int> DeletePostAsync(int id)
        {
            return await _dbContext.PostEntities.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<List<PostEntity>> GetAllPostsAsync()
        {
            return await _dbContext.PostEntities.AsNoTracking().ToListAsync();
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {
           return await _dbContext.PostEntities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<int> UpdatePostAsync(int id, PostEntity entity)
        {
            return await _dbContext.PostEntities
                         .Where(x => x.Id == id)
                         .ExecuteUpdateAsync(setters => setters
                         .SetProperty(u => u.Id, entity.Id)
                         .SetProperty(u => u.Title, entity.Title)
                         .SetProperty(u => u.Description, entity.Description));
        }
    }
}
