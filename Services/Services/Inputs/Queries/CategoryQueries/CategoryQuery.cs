using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Queries.CategoryQueries
{
    public class CategoryQuery : IRequest<CategoryVM>
    {
        public int Id { get; set; }
    }
}
