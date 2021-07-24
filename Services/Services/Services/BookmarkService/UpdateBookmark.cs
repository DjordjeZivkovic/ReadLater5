using AutoMapper;
using MediatR;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.BookmarkService
{
    public class UpdateBookmark
    {
        public class Handler : IRequestHandler<BookmarkUpdateCommand, BookmarkVM>
        {
            private readonly IDataContext _context;
            public readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<BookmarkVM> Handle(BookmarkUpdateCommand request, CancellationToken cancellationToken)
            {
                var bookmark = await _context.Bookmarks.FindAsync(request.Bookmark.Id);

                if (bookmark == null)
                    throw new Exception(Errors.NotFound);

                _mapper.Map(request.Bookmark, bookmark);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success)
                    throw new Exception(Errors.ProblemSavingChanges);

                return _mapper.Map<BookmarkVM>(bookmark);
            }
        }
    }
}
