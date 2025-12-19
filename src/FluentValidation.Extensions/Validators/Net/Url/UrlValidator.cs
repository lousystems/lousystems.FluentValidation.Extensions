using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net;

public class UrlValidator<T> : PropertyValidator<T, string?>
{
    public override string Name => "UrlValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return string.IsNullOrWhiteSpace(value) || Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid URL.";
}