using System.Net;
using System.Net.Sockets;
using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net;

public class Ipv4Validator<T> : PropertyValidator<T, string?>
{
    public override string Name => "IPv4Validator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true; // We let 'NotEmpty' or 'NotNull' handle empty values

        if (IPAddress.TryParse(value, out var address))
        {
            return address.AddressFamily == AddressFamily.InterNetwork;
        }

        return false;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid IPv4 address (e.g. 192.168.1.1).";
}