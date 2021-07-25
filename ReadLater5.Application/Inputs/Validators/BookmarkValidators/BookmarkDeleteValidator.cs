using FluentValidation;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;

namespace ReadLater5.Application.Inputs.Validators.BookmarkValidators
{
    class BookmarkDeleteValidator : AbstractValidator<BookmarkDeleteCommand>
    {
        public BookmarkDeleteValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
