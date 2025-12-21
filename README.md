[![Build & Tests](https://github.com/lousystems/lousystems.FluentValidation.Extensions/actions/workflows/tests.yml/badge.svg)](https://github.com/lousystems/lousystems.FluentValidation.Extensions/actions/workflows/tests.yml)

# lousystems.FluentValidation.Extensions

A high-performance, **Native AOT-friendly** collection of validation extensions for [FluentValidation](https://github.com/FluentValidation/FluentValidation).

This library focuses on providing optimized, reusable rules for modern .NET applications (Cloud-Native, APIs, Microservices) without the overhead of heavy Regex where simpler parsing suffices.
## 🚀 Features
- **Network Validators**: IPv4 and IPv6 address validation and Url validation. For Infomation check [here](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Net/Net.md).
- **Security & Identity Validators**: NATS NKey and JWT structural validation. For Infomation check [here](https://github.com/lousystems/lousystems.FluentValidation.Extensions/blob/master/src/FluentValidation.Extensions/Validators/Security/Security.md).
## 📦 Installation

Install via the NuGet Package Manager:

```powershell
Install-Package lousystems.FluentValidation.Extensions

```

Or via the .NET CLI:

```bash
dotnet add package lousystems.FluentValidation.Extensions

```

## 💻 Usage


```csharp
using FluentValidation;
using lousystems.FluentValidation.Extensions;

public class ServerConfigurationValidator : AbstractValidator<ServerConfiguration>
{
    public ServerConfigurationValidator()
    {
        // Validates standard IPv4 format (e.g. 192.168.1.1)
        RuleFor(x => x.PublicIp)
            .NotEmpty()
            .MustBeIPv4();

        // Validates standard IPv6 format (e.g. 2001:db8::1)
        RuleFor(x => x.ManagementIp)
            .MustBeIPv6();
    }
}


```

## 🛠 Building from Source

To build and test this project locally, ensure you have the **.NET 10 SDK** (or latest .NET 8/9 SDK) installed.

```bash
# Clone the repository
git clone https://github.com/lousystems/lousystems.FluentValidation.Extensions.git

# Navigate to the solution
cd FluentValidation.Extensions

# Restore dependencies
dotnet restore

# Build
dotnet build --configuration Release

# Run Unit Tests
dotnet test

```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature/my-new-validator`).
3. Commit your changes.
4. **Add Unit Tests** for your new extensions.
5. Push to the branch and open a Pull Request.

Please ensure your code follows the existing style and that all tests pass.

## 📄 License

This project is licensed under the **Apache 2.0 License**. See the [LICENSE](https://www.google.com/search?q=LICENSE) file for details.

---

**lousystems** © 2025

---
