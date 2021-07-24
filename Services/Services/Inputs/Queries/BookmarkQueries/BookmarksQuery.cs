using MediatR;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Envelopes;
using System.Collections.Generic;

namespace ReadLater5.Application.Inputs.Queries.BookmarkQueries
{
    public class BookmarksQuery : IRequest<BookmarkEnvelope>
    {
        public int? Start { get; set; }
        public int? Length { get; set; }
        public string Search { get; set; }
        public IEnumerable<ColumnDto> Columns { get; set; }
    }
}
