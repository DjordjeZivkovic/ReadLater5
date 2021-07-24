using FluentValidation;
using ReadLater5.Application.Inputs.Commands.CategoryCommands;

namespace ReadLater5.Application.Inputs.Validators.CategoryValidators
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateCommand>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Category.Id).NotNull();
            RuleFor(x => x.Category.Name).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
