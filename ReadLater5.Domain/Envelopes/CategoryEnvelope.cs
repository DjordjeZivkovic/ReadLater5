using ReadLater5.Domain.Dtos;
using System.Collections.Generic;

namespace ReadLater5.Domain.Envelopes
{
    public class CategoryEnvelope
    {
        public List<CategoryDto> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
