using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net;

public class AbsoluteUrlValidator<T> : PropertyValidator<T, string?>
{
    public override string Name => "AbsoluteUrlValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return string.IsNullOrWhiteSpace(value) || Uri.TryCreate(value, UriKind.Absolute, out _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid absolute URL (e.g. https://example.com).";
}