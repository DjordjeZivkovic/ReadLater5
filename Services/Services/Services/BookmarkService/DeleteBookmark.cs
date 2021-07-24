using MediatR;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.BookmarkService
{
    public class DeleteBookmark
    {
        public class Handler : IRequestHandler<BookmarkDeleteCommand>
        {
            private readonly IDataContext _context;

            public Handler(IDataContext context) => 
                _context = context;

            public async Task<Unit> Handle(BookmarkDeleteCommand request, CancellationToken cancellationToken)
            {
                var bookmark = await _context.Bookmarks.FindAsync(request.Id);

                if (bookmark == null)
                    throw new Exception(Errors.NotFound);

                _context.Bookmarks.Remove(bookmark);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success)
                    throw new Exception(Errors.ProblemSavingChanges);

                return Unit.Value;
            }
        }
    }
}
