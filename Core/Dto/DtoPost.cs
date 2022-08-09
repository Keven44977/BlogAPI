namespace Core.Dto
{
    public class DtoPost
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? Content { get; set; }
        public int CategoryId { get; set; }
    }
}
