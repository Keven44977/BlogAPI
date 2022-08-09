using Core.Entity;
using Repositories.RepositoryBase;

namespace Repositories.CategoriesRepository
{
    public interface ICategoriesRepository : IRepositoryBase<Category, int>
    {
        Category GetByTitle(string? title);
    }
}
