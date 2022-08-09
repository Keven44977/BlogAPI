namespace Core.Model.Request
{
    public class PostModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? PublicationDate { get; set; }
        public string? Content { get; set; }
        public int CategoryId { get; set; }
    }
}
