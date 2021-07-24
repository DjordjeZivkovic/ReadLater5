using MediatR;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Envelopes;
using System.Collections.Generic;

namespace ReadLater5.Application.Inputs.Queries.CategoryQueries
{
    public class CategoriesQuery : IRequest<CategoryEnvelope>
    {
        public int? Start { get; set; }
        public int? Length { get; set; }
        public string Search { get; set; }
        public IEnumerable<ColumnDto> Columns { get; set; }
    }
}
