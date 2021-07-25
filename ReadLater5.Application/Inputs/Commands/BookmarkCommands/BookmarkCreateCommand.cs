using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Commands.BookmarkCommands
{
    public class BookmarkCreateCommand : IRequest
    {
        public BookmarkVM Bookmark { get; set; }
    }
}
