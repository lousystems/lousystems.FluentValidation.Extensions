[![Build & Tests](https://github.com/lousystems/lousystems.FluentValidation.Extensions/actions/workflows/tests.yml/badge.svg)](https://github.com/lousystems/lousystems.FluentValidation.Extensions/actions/workflows/tests.yml)
# lousystems.FluentValidation.Extensions
A collection of lightweight, reusable validation extensions for **FluentValidation**. Designed to simplify complex business rules and maintain clean, readable validators in modern .NET applications.



---

## 🛠 Features & Usage

This library extends `IRuleBuilder` to provide specialized validation logic out of the box.

### 1. Validating Network-related Fields
#### IP Address Validation
Strictly validates IPv4 and IPv6 addresses.
More details can be found in the [Net Documentation](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Net/Net.md).


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
#### URL Validation
Validates absolute and relative URLs.
More details can be found in the [Net Documentation](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Net/Net.md).

### 2. Security & Identity Validation
#### JWT Validation
Performs structural validation of JSON Web Tokens (JWT).
More details can be found in the [Security & Identity Validation Documentation](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Security/Security.md).

### NATS NKey Validation
Validates NATS NKeys ensuring correct prefixes and lengths.
More details can be found in the [Security & Identity Validation Documentation](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Security/Security.md)

## 🏗 Technical Specifications

* **Target Frameworks:**  .NET 10.0 (LTS).
* **Dependency:** [FluentValidation](https://www.nuget.org/packages/FluentValidation) (>= 12.0.0).
* **Native AOT Ready:** This library does not use reflection at runtime and is fully compatible with **Native AOT** (Ahead-of-Time) compilation for high-performance cloud-native workloads.

---

## ⚖ License

This project is licensed under the **MIT License**. See the `LICENSE` file for more information.

## 🤝 Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request on our [GitHub Repository](https://github.com/lousystems/lousystems.FluentValidation.Extensions).

---

### Technical Implementation Note

When using the `IfNotNull` variants, the library handles the internal state by wrapping rules in a `When` condition. This keeps your validator code clean while ensuring strict type safety and null-handling.
