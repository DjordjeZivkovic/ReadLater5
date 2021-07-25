using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReadLater5.Application.Inputs.Queries.BookmarkQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Envelopes;
using ReadLater5.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.BookmarkService
{
    public class GetBookmarks
    {
        public class Handler : IRequestHandler<BookmarksQuery, BookmarkEnvelope>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookmarkEnvelope> Handle(BookmarksQuery request, CancellationToken cancellationToken)
            {
                var queryable = _context.Bookmarks.AsQueryable();

                if (!string.IsNullOrEmpty(request.Search))
                    queryable = queryable.Where(x => x.ShortDescription.Contains(request.Search) || x.URL.Contains(request.Search));

                IEnumerable<Bookmark> bookmarks = await queryable
                    .Skip(request.Start ?? 0)
                    .Take(request.Length ?? 10)
                    .ToListAsync();

                return new BookmarkEnvelope
                {
                    Data = _mapper.Map<List<BookmarkDto>>(bookmarks),
                    RecordsFiltered = queryable.Count(),
                    RecordsTotal = queryable.Count()
                };
            }
        }
    }
}
