using Core.Entity;
using Database;
using Repositories.RepositoryBase;

namespace Repositories.PostsRepository
{
    public class PostsRepository : RepositoryBase<Post, int>, IPostsRepository
    {
        public PostsRepository(BlogContext context) : base(context)
        {

        }

        public List<Post> GetByCategory(int categoryId)
        {
            return _context.Posts?.Where(post => post.CategoryId == categoryId).ToList();
        }

        public List<Post> GetAllPriorOrTodayDescending()
        {
            return _context.Posts?.Where(post => post.PublicationDate <= DateTime.UtcNow).OrderByDescending(post => post.PublicationDate).ToList();
        }

        public Post GetByTitle(string title)
        {
            return _context.Posts?.Where(post => post.Title == title).FirstOrDefault();
        }
    }
}
