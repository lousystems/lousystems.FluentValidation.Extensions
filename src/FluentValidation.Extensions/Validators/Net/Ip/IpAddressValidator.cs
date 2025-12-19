using System.Net;
using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net;

public class IpAddressValidator<T> : PropertyValidator<T, string?>
{
    public override string Name => "IpAddressValidator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        return string.IsNullOrWhiteSpace(value) ||
               IPAddress.TryParse(value, out _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid IP address.";
}