

### IP Address Validation

Strictly validates IPv4 and IPv6 addresses using the high-performance `System.Net.IPAddress` parser (no Regex). These extensions distinguish correctly between address families, ensuring an IPv6 address does not pass an IPv4 check and vice versa.

#### Usage

```csharp
public class NetworkConfigValidator : AbstractValidator<NetworkConfig>
{
    public NetworkConfigValidator()
    {
        // Validates standard IPv4 format (e.g., 192.168.1.1)
        RuleFor(x => x.PrimaryIp).MustBeIPv4();

        // Validates standard IPv6 format (e.g., 2001:0db8::1)
        RuleFor(x => x.SecondaryIp).MustBeIPv6();
        
        // Combine with NotEmpty if the field is mandatory
        RuleFor(x => x.Gateway).NotEmpty().MustBeIPv4();
    }
}

```

#### Technical Behavior

* **Performance:** Uses `IPAddress.TryParse` internally, making it **Native AOT friendly** and significantly faster than Regular Expressions.
* **Strict Typing:** Checks `AddressFamily.InterNetwork` (IPv4) vs `AddressFamily.InterNetworkV6` (IPv6) to prevent cross-family false positives (e.g., `::1` will fail `MustBeIPv4`).
* **Null Handling:** Returns `true` for `null` or empty strings to allow optional fields (standard FluentValidation behavior). Use `.NotEmpty()` to enforce presence.

---
