using MediatR;

namespace ReadLater5.Application.Inputs.Commands.BookmarkCommands
{
    public class BookmarkDeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}
