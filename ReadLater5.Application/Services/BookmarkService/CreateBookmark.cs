using AutoMapper;
using MediatR;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.BookmarkService
{
    public class CreateBookmark
    {
        public class Handler : IRequestHandler<BookmarkCreateCommand>
        {
            private readonly IDataContext _context;
            public readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(BookmarkCreateCommand request, CancellationToken cancellationToken)
            {
                var bookmark = _mapper.Map<Bookmark>(request.Bookmark);

                _context.Bookmarks.Add(bookmark);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success)
                    throw new Exception(Errors.ProblemSavingChanges);

                return Unit.Value;
            }
        }
    }
}
