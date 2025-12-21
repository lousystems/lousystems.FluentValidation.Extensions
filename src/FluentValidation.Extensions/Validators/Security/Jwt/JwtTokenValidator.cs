using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Security.Jwt;

public class JwtTokenValidator<T> : PropertyValidator<T, string?>
{
    public override string Name => "JwtTokenValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;

        var parts = value.Split('.');
        return parts.Length == 3 &&
               !string.IsNullOrWhiteSpace(parts[0]) && 
               !string.IsNullOrWhiteSpace(parts[1]) && 
               !string.IsNullOrWhiteSpace(parts[2]);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid JWT token.";
}