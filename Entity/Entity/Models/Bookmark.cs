using System.ComponentModel.DataAnnotations;

namespace ReadLater5.Domain.Models
{
    public class Bookmark : BaseModel
    {
        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
