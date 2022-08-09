using Core.Entity;

namespace BlogAPI.Services
{
    public interface IPostsService
    {
        List<Post> GetAll();
        List<Post> GetAllPriorOrTodayDescending();
        Post Create(Post post);
        Post GetById(int id);
        Post GetByTitle(string title);
        List<Post> GetByCategory(int categoryId);
        Post Update(Post post);
        void Delete(int id);
    }
}
