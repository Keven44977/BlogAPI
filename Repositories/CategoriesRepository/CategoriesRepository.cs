using Core.Entity;
using Database;
using Repositories.RepositoryBase;

namespace Repositories.CategoriesRepository
{
    public class CategoriesRepository : RepositoryBase<Category, int>, ICategoriesRepository
    {
        public CategoriesRepository(BlogContext context) : base(context)
        {
        }

        public Category GetByTitle(string? title)
        {
            return _context.Categories?.Where(category => category.Title == title).FirstOrDefault();
        }
    }
}
