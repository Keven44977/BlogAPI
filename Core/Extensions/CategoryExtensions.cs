using Core.Dto;
using Core.Entity;

namespace Core.Extensions
{
    public static class CategoryExtensions
    {
        public static DtoCategory ToDto(this Category category)
        {
            if (category == null)
            {
                return null;
            }

            return new DtoCategory()
            {
                Id = category.Id,
                Title = category.Title
            };
        }

        public static Category ToEntity(this DtoCategory dtoCategory)
        {
            if (dtoCategory == null)
            {
                return null;
            }

            return new Category()
            {
                Id = dtoCategory.Id,
                Title = dtoCategory.Title
            };
        }

        public static List<DtoCategory> ToDto(this IEnumerable<Category> categories)
        {
            if(categories == null)
            {
                return null;
            }

            List<DtoCategory> dtoCategories = new List<DtoCategory>();

            foreach (Category category in categories)
            {
                dtoCategories.Add(category.ToDto());
            }

            return dtoCategories;
        }

        public static List<Category> ToEntity(this IEnumerable<DtoCategory> dtoCategories)
        {
            if(dtoCategories == null)
            {
                return null;
            }

            List<Category> categories = new List<Category>();

            foreach (DtoCategory dtoCategory in dtoCategories)
            {
                categories.Add(dtoCategory.ToEntity());
            }

            return categories;
        }
    }
}
