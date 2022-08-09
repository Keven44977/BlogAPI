using BlogAPI.Services;
using Core.Dto;
using Core.Entity;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IPostsService _postService;

        public CategoriesController(ICategoriesService categoriesService, IPostsService postsService)
        {
            _categoriesService = categoriesService;
            _postService = postsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<DtoCategory> allCategories = _categoriesService.GetAll().ToDto();

            if (allCategories == null || allCategories.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(allCategories);
            }
        }

        [HttpGet]
        [Route("/Categories/{id}")]
        public IActionResult Get(int id)
        {
            Category requestedCategory = _categoriesService.GetById(id);

            if (requestedCategory == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(requestedCategory.ToDto());
            }
        }

        [HttpGet]
        [Route("/Categories/{id}/post")]
        public IActionResult GetPostsByCategory(int id)
        {
            List<Post> postsInCateogry = _postService.GetByCategory(id);

            if (postsInCateogry == null || postsInCateogry.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(postsInCateogry.ToDto());
            }
        }

        [HttpPost]
        public IActionResult Create(string title)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                return BadRequest();
            }

            bool categoryExists = _categoriesService.GetByTitle(title) != null;

            if (categoryExists)
            {
                return Conflict();
            }

            Category newCategory = new Category()
            {
                Title = title
            };

            Category createdCategory = _categoriesService.Create(newCategory);

            return Ok(createdCategory.ToDto());
        }

        [HttpPut]
        public IActionResult Update([FromBody] DtoCategory dtoCategory)
        {
            if (dtoCategory == null || string.IsNullOrEmpty(dtoCategory.Title) || string.IsNullOrWhiteSpace(dtoCategory.Title))
            {
                return BadRequest();
            }

            Category categoryToUpdate = _categoriesService.GetById(dtoCategory.Id);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            Category existingCategory = _categoriesService.GetByTitle(dtoCategory.Title);

            bool categoryExists = existingCategory != null && existingCategory.Id != dtoCategory.Id;

            if (categoryExists)
            {
                return Conflict();
            }

            categoryToUpdate.Title = dtoCategory.Title;

            Category updatedCategory = _categoriesService.Update(categoryToUpdate);

            return Ok(updatedCategory);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Category categoryToDelete = _categoriesService.GetById(id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            List<Post> postsInCategory = _postService.GetByCategory(id);

            if (postsInCategory != null)
            {
                foreach (Post post in postsInCategory)
                {
                    _postService.Delete(post.Id);
                }
            }

            _categoriesService.Delete(categoryToDelete.Id);

            return Ok();
        }
    }
}
