using MediatR;
using ReadLater5.Domain.ViewModels;

namespace ReadLater5.Application.Inputs.Commands.CategoryCommands
{
    public class CategoryUpdateCommand : IRequest
    {
        public CategoryVM Category { get; set; }
    }
}
