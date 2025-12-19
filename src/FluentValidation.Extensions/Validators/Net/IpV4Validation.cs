using System.Net;
using System.Net.Sockets;

namespace FluentValidation.Extensions.Validators.Net;

public static class IpAddressExtensions
{
    extension<T>(IRuleBuilder<T, string?> ruleBuilder)
    {
        /// <summary>
        /// Validates that the string is a valid IPv4 address.
        /// </summary>
        public IRuleBuilderOptions<T, string?> MustBeIPv4()
        {
            return ruleBuilder.Must(ip => 
                {
                    if (string.IsNullOrWhiteSpace(ip)) 
                        return true; // We let 'NotEmpty' or 'NotNull' handle empty values

                    // TryParse is highly optimized and AOT-friendly
                    if (IPAddress.TryParse(ip, out var address))
                    {
                        // Ensure it is specifically an IPv4 address (InterNetwork)
                        return address.AddressFamily == AddressFamily.InterNetwork;
                    }

                    return false;
                })
                .WithMessage("'{PropertyName}' must be a valid IPv4 address (e.g. 192.168.1.1).");
        }

        /// <summary>
        /// Validates that the string is a valid IPv6 address.
        /// </summary>
        public IRuleBuilderOptions<T, string?> MustBeIPv6()
        {
            return ruleBuilder.Must(ip => 
                {
                    if (string.IsNullOrWhiteSpace(ip)) 
                        return true; // We let 'NotEmpty' or 'NotNull' handle empty values

                    // TryParse is highly optimized and AOT-friendly
                    if (IPAddress.TryParse(ip, out var address))
                    {
                        // Ensure it is specifically an IPv4 address (InterNetwork)
                        return address.AddressFamily == AddressFamily.InterNetworkV6;
                    }

                    return false;
                })
                .WithMessage("'{PropertyName}' must be a valid IPv6 address (e.g. 2001:0db8:85a3:0000:0000:8a2e:0370:7334).");
        }
    }
}