namespace ReadLater5.Domain.ViewModels
{
    public class BookmarkVM
    {
        public int Id { get; set; }

        public string URL { get; set; }

        public string ShortDescription { get; set; }
        public int? CategoryId { get; set; }

        public virtual CategoryVM Category { get; set; }
    }
}
