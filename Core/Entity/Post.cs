using Core.Model;

namespace Core.Entity
{
    public class Post : BaseEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? Content { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
