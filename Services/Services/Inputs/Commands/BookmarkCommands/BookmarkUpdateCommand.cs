using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Commands.BookmarkCommands
{
    public class BookmarkUpdateCommand : IRequest<BookmarkVM>
    {
        public BookmarkVM Bookmark { get; set; }
    }
}
