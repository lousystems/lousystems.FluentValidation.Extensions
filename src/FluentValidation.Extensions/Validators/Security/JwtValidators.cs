using FluentValidation.Extensions.Validators.Security.Jwt;

namespace FluentValidation.Extensions.Validators.Security;



public static class JwtValidators
{
    extension<T>(IRuleBuilder<T, string?> ruleBuilder)
    {
        public IRuleBuilderOptions<T, string?> IsJwtToken()
            => ruleBuilder.SetValidator(new JwtTokenValidator<T>());
    }
}