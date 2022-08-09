using Core.Entity;
using Repositories.CategoriesRepository;

namespace BlogAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public Category Create(Category category)
        {
            if (category == null)
            {
                return null;
            }

            _categoriesRepository.Add(category);
            return category;
        }

        public void Delete(int id)
        {
            Category categoryToDelete = _categoriesRepository.GetById(id);

            if (categoryToDelete != null)
            {
                _categoriesRepository.Remove(categoryToDelete);
            }
        }

        public List<Category> GetAll()
        {
            return _categoriesRepository.GetAll().ToList();
        }

        public Category GetById(int id)
        {
            return _categoriesRepository.GetById(id);
        }

        public Category GetByTitle(string? title)
        {
            return _categoriesRepository.GetByTitle(title);
        }

        public Category Update(Category category)
        {
            _categoriesRepository.Update(category);
            return category;
        }
    }
}
