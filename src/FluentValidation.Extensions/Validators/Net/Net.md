
For URL scroll down


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

### URL Validation

Provides comprehensive validation for Uniform Resource Locators, supporting both absolute URLs (e.g., website links) and relative URLs (e.g., API paths).

#### Usage

```csharp
public class LinkValidator : AbstractValidator<LinkDto>
{
    public LinkValidator()
    {
        // Enforces absolute URLs (must include scheme like http, https, ftp)
        // Valid: "https://www.example.com"
        // Invalid: "/api/users", "www.google.com" (missing scheme)
        RuleFor(x => x.Website).IsAbsoluteUrl();

        // Enforces relative URLs (paths without scheme/host)
        // Valid: "/assets/logo.png", "../images/file.jpg"
        // Invalid: "http://localhost:5000"
        RuleFor(x => x.AvatarPath).IsRelativeUrl();
        
        // Allows both valid absolute and relative URLs
        RuleFor(x => x.RedirectTarget).IsUrl();
    }
}
```

#### Technical Behavior

* **Absolute URLs:** Leverages `Uri.TryCreate` with `UriKind.Absolute` to ensure full compliance with RFC 3986. This ensures the URL contains a valid scheme and authority.
* **Relative URLs:** Uses a specialized Regex pattern to validate paths, ensuring they are well-formed relative identifiers without protocol prefixes.
* **Null Handling:** Returns `true` for `null` or empty strings to allow optional fields. Use `.NotEmpty()` to enforce presence.


