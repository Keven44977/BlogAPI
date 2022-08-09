using Core.Model;

namespace Core.Entity
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<Post>? Post { get; set; }
    }
}
