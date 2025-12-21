using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net.Url;

public partial class RelativeUrlRegexValidator<T> : PropertyValidator<T, string?>
{
    [GeneratedRegex(@"^(?!www\.|(?:http|ftp)s?://|[A-Za-z]:\\|//).*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)]
    private static partial Regex RelativeUrlRegex();

    public override string Name => "RelativeUrlValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return string.IsNullOrWhiteSpace(value) || RelativeUrlRegex().IsMatch(value);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a relative URL.";
}