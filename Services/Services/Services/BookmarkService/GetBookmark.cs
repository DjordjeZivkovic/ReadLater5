using AutoMapper;
using MediatR;
using ReadLater5.Application.Inputs.Queries.BookmarkQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.BookmarkService
{
    public class GetBookmark
    {
        public class Handler : IRequestHandler<BookmarkQuery, BookmarkVM>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookmarkVM> Handle(BookmarkQuery request, CancellationToken cancellationToken)
            {
                var bookmark = await _context.Bookmarks.FindAsync(request.Id);

                if (bookmark == null)
                    throw new Exception(Errors.NotFound);

                return _mapper.Map<BookmarkVM>(bookmark);
            }
        }
    }
}
