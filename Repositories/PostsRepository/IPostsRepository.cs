using Core.Entity;
using Repositories.RepositoryBase;

namespace Repositories.PostsRepository
{
    public interface IPostsRepository : IRepositoryBase<Post, int>
    {
        List<Post> GetByCategory(int categoryId);
        List<Post> GetAllPriorOrTodayDescending();
        Post GetByTitle(string title);
    }
}
