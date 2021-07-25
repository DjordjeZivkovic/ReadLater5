using FluentValidation;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;

namespace ReadLater5.Application.Inputs.Validators.BookmarkValidators
{
    class BookmarkUpdateValidator : AbstractValidator<BookmarkUpdateCommand>
    {
        public BookmarkUpdateValidator()
        {
            RuleFor(x => x.Bookmark.Id).NotNull();
            RuleFor(x => x.Bookmark.URL).NotNull().NotEmpty().MaximumLength(500);
            RuleFor(x => x.Bookmark.ShortDescription).NotNull().NotEmpty();
        }
    }
}
