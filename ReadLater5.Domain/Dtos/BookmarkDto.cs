namespace ReadLater5.Domain.Dtos
{
    public class BookmarkDto
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public CategoryDto Category { get; set; }
    }
}
