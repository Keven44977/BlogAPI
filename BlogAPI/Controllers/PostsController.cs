using BlogAPI.Services;
using Core.Entity;
using Core.Extensions;
using Core.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postService;

        public PostsController(IPostsService postsService)
        {
            _postService = postsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Post> allPosts = _postService.GetAllPriorOrTodayDescending();

            if (allPosts == null || allPosts.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(allPosts.ToDto());
            }
        }

        [HttpGet]
        [Route("/Posts/{id}")]
        public IActionResult Get(int id)
        {
            Post requestedPost = _postService.GetById(id);

            if (requestedPost == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(requestedPost.ToDto());
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PostModel post)
        {
            if (post == null || 
                string.IsNullOrEmpty(post.Title) || string.IsNullOrWhiteSpace(post.Title) ||
                 string.IsNullOrEmpty(post.Content) || string.IsNullOrWhiteSpace(post.Content) ||
                post.PublicationDate == null)
            {
                return BadRequest();
            }

            bool postExists = _postService.GetByTitle(post.Title) != null;

            if (postExists)
            {
                return Conflict();
            }

            Post newPost = new Post()
            {
                Title = post.Title,
                Content = post.Content,
                PublicationDate = DateTime.Parse(post.PublicationDate),
                CategoryId = post.CategoryId
            };

            Post createdCategory = _postService.Create(newPost);

            return Ok(createdCategory.ToDto());
        }

        [HttpPut]
        public IActionResult Update([FromBody] PostModel post)
        {
            if (post == null || 
                string.IsNullOrEmpty(post.Title) || string.IsNullOrWhiteSpace(post.Title) || 
                string.IsNullOrEmpty(post.Content) || string.IsNullOrWhiteSpace(post.Content) || 
                post.PublicationDate == null)
            {
                return BadRequest();
            }

            Post postToUpdate = _postService.GetById(post.Id);

            if (postToUpdate == null)
            {
                return NotFound();
            }

            Post existingPost = _postService.GetByTitle(post.Title);

            bool postExists = existingPost != null && existingPost.Id != post.Id;

            if (postExists)
            {
                return Conflict();
            }

            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            postToUpdate.PublicationDate = DateTime.Parse(post.PublicationDate);
            postToUpdate.CategoryId = post.CategoryId;

            Post updatedPost = _postService.Update(postToUpdate);

            return Ok(updatedPost);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Post postToDelete = _postService.GetById(id);

            if (postToDelete == null)
            {
                return NotFound();
            }

            _postService.Delete(postToDelete.Id);

            return Ok();
        }
    }
}
