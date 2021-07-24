using FluentValidation;
using ReadLater5.Application.Inputs.Commands.CategoryCommands;

namespace ReadLater5.Application.Inputs.Validators.CategoryValidators
{
    class CategoryCreateValidator : AbstractValidator<CategoryCreateCommand>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Category.Name).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}
