namespace ReadLater5.Domain.Dtos
{
    public class ColumnDto
    {
        public string Data { get; set; }
        public SearchDto Search { get; set; }
    }

    public class SearchDto
    {
        public string Value { get; set; }
    }
}
