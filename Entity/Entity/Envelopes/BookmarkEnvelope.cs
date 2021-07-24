using ReadLater5.Domain.Dtos;
using System.Collections.Generic;

namespace ReadLater5.Domain.Envelopes
{
    public class BookmarkEnvelope
    {
        public List<BookmarkDto> Data { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
