
# lousystems.FluentValidation.Extensions

A collection of lightweight, reusable validation extensions for **FluentValidation**. Designed to simplify complex business rules and maintain clean, readable validators in modern .NET applications.


---

## 🛠 Features & Usage

This library extends `IRuleBuilder` to provide specialized validation logic out of the box.

### 1. Validating Network-related Fields
#### IP Address Validation
Strictly validates IPv4 and IPv6 addresses using the high-performance `System.Net.IPAddress` parser (no Regex). These extensions distinguish correctly between address families, ensuring an IPv6 address does not pass an IPv4 check and vice versa.
More details can be found in the [IP Address Validation Documentation](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Net/Net.md).


```csharp
public class MyRequestValidator : AbstractValidator<MyRequest>
{
    public MyRequestValidator()
    {
        // Mandatory field
        RuleFor(x => x.ServerIp).MustBeIPv4();
    }
}

```


## 🏗 Technical Specifications

* **Target Frameworks:**  .NET 10.0 (LTS).
* **Dependency:** [FluentValidation](https://www.nuget.org/packages/FluentValidation) (>= 12.0.0).
* **Native AOT Ready:** This library does not use reflection at runtime and is fully compatible with **Native AOT** (Ahead-of-Time) compilation for high-performance cloud-native workloads.

---

## ⚖ License

This project is licensed under the **MIT License**. See the `LICENSE` file for more information.

## 🤝 Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request on our [GitHub Repository](https://github.com/lousystems/FluentValidation.Extensions).

---

### Technical Implementation Note

When using the `IfNotNull` variants, the library handles the internal state by wrapping rules in a `When` condition. This keeps your validator code clean while ensuring strict type safety and null-handling.
