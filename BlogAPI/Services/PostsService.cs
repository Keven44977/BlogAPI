using Core.Entity;
using Repositories.PostsRepository;

namespace BlogAPI.Services
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public Post Create(Post post)
        {
            if (post == null)
            {
                return null;
            }

            _postsRepository.Add(post);
            return post;
        }

        public void Delete(int id)
        {
            Post postToDelete = _postsRepository.GetById(id);

            if (postToDelete != null)
            {
                _postsRepository.Remove(postToDelete);
            }
        }

        public List<Post> GetAll()
        {
            return _postsRepository.GetAll().ToList();
        }

        public List<Post> GetAllPriorOrTodayDescending()
        {
            return _postsRepository.GetAllPriorOrTodayDescending();
        }

        public List<Post> GetByCategory(int categoryId)
        {
            return _postsRepository.GetByCategory(categoryId);
        }

        public Post GetById(int id)
        {
            return _postsRepository.GetById(id);
        }

        public Post GetByTitle(string title)
        {
            return _postsRepository.GetByTitle(title);
        }

        public Post Update(Post post)
        {
            _postsRepository.Update(post);

            return post;
        }
    }
}
