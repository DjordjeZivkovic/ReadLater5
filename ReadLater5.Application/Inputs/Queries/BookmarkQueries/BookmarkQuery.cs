using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Queries.BookmarkQueries
{
    public class BookmarkQuery : IRequest<BookmarkVM>
    {
        public int Id { get; set; }
    }
}
