using FluentValidation;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;

namespace ReadLater5.Application.Inputs.Validators.BookmarkValidators
{
    public class BookmarkCreateValidator : AbstractValidator<BookmarkCreateCommand>
    {
        public BookmarkCreateValidator()
        {
            RuleFor(x => x.Bookmark.URL).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.Bookmark.ShortDescription).NotNull().NotEmpty();
        }
    }
}
