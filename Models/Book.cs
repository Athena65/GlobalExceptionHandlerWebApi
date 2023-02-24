namespace GlobalExceptionHandlerWebApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Pages { get; set; }
        public int ISBN13 { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
    }
}
