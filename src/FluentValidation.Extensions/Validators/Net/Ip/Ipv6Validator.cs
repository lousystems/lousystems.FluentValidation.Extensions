using System.Net;
using System.Net.Sockets;
using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Net;


public class Ipv6Validator<T> : PropertyValidator<T, string?>
{
    public override string Name => "IPv6Validator";

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true; 

        if (IPAddress.TryParse(value, out var address))
        {
            return address.AddressFamily == AddressFamily.InterNetworkV6;
        }

        return false;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid IPv6 address (e.g. 2001:0db8:85a3:0000:0000:8a2e:0370:7334).";
}