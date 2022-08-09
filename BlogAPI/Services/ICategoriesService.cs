using Core.Entity;

namespace BlogAPI.Services
{
    public interface ICategoriesService
    {
        List<Category> GetAll();
        Category Create(Category category);
        Category GetById(int id);
        Category GetByTitle(string? title);
        Category Update(Category category);
        void Delete(int id);
    }
}
