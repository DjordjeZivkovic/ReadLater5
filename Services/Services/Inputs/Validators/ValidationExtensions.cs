using FluentValidation;

namespace ReadLater5.Application.Inputs.Validators
{
    public static class ValidationExtensions
    {
        public static IRuleBuilder<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.NotNull().NotEmpty().EmailAddress();
    }
}
