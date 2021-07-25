using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Commands.CategoryCommands
{
    public class CategoryCreateCommand : IRequest
    {
        public CategoryVM Category { get; set; }
    }
}
