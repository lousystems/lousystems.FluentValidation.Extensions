
# lousystems.FluentValidation.Extensions

A collection of lightweight, reusable validation extensions for **FluentValidation**. Designed to simplify complex business rules and maintain clean, readable validators in modern .NET applications.

## 🚀 Installation

Install the package via .NET CLI:

```bash
dotnet add package lousystems.FluentValidation.Extensions

```

Or via the NuGet Package Manager:

```powershell
Install-Package lousystems.FluentValidation.Extensions

```

---

## 🛠 Features & Usage

This library extends `IRuleBuilder` to provide specialized validation logic out of the box.

### 1. Service Name Validation

Validates strings against common cloud-resource naming conventions (e.g., lowercase, hyphens, specific length).

```csharp
public class MyRequestValidator : AbstractValidator<MyRequest>
{
    public MyRequestValidator()
    {
        // Mandatory field
        RuleFor(x => x.ServiceName).MustBeServiceName();

        // Optional field: Only validates if not null
        RuleFor(x => x.OptionalService).MustBeServiceNameIfNotNull();
    }
}

```

### 2. Conditional Logic

The extensions are built using `ApplyConditionTo.AllValidators`, ensuring that entire rule chains are correctly skipped when properties are `null`, preventing unintended validation failures.

---

## 🏗 Technical Specifications

* **Target Frameworks:** .NET 8.0, .NET 9.0, and .NET 10.0 (LTS).
* **Dependency:** [FluentValidation](https://www.nuget.org/packages/FluentValidation) (>= 11.0.0).
* **Native AOT Ready:** This library does not use reflection at runtime and is fully compatible with **Native AOT** (Ahead-of-Time) compilation for high-performance cloud-native workloads.

---

## ⚖ License

This project is licensed under the **MIT License**. See the `LICENSE` file for more information.

## 🤝 Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue or submit a pull request on our [GitHub Repository](https://github.com/lousystems/FluentValidation.Extensions).

---

### Technical Implementation Note

When using the `IfNotNull` variants, the library handles the internal state by wrapping rules in a `When` condition. This keeps your validator code clean while ensuring strict type safety and null-handling.

**Would you like me to add a specific section for Unit Testing these extensions to show your users how to verify their rules?**