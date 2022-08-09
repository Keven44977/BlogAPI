using Core.Dto;
using Core.Entity;

namespace Core.Extensions
{
    public static class PostExtensions
    {
        public static DtoPost ToDto(this Post post)
        {
            if (post == null)
            {
                return null;
            }

            return new DtoPost()
            {
                Id = post.Id,
                Title = post.Title,
                PublicationDate = post.PublicationDate,
                CategoryId = post.CategoryId,
                Content = post.Content,
            };
        }

        public static Post ToEntity(this DtoPost dtoPost)
        {
            if (dtoPost == null)
            {
                return null;
            }

            return new Post()
            {
                Id = dtoPost.Id,
                Title = dtoPost.Title,
                PublicationDate = dtoPost.PublicationDate,
                CategoryId = dtoPost.CategoryId,
                Content = dtoPost.Content,
            };
        }

        public static List<DtoPost> ToDto(this IEnumerable<Post> posts)
        {
            if (posts == null)
            {
                return null;
            }

            List<DtoPost> dtoPosts = new List<DtoPost>();

            foreach (Post post in posts)
            {
                dtoPosts.Add(post.ToDto());
            }

            return dtoPosts;
        }

        public static List<Post> ToEntity(this IEnumerable<DtoPost> dtoPosts)
        {
            if (dtoPosts == null)
            {
                return null;
            }

            List<Post> posts = new List<Post>();

            foreach (DtoPost dtoPost in dtoPosts)
            {
                posts.Add(dtoPost.ToEntity());
            }

            return posts;
        }
    }
}
