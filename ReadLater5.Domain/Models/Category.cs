using System.ComponentModel.DataAnnotations;

namespace ReadLater5.Domain.Models
{
    public class Category : BaseModel
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
    }
}
