using FluentValidation;
using ReadLater5.Application.Inputs.Queries.AuthQueries;

namespace ReadLater5.Application.Inputs.Validators.AuthValidators
{
    class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
