using FluentValidation.Extensions.Validators.Net;
using FluentValidation.TestHelper;

namespace FluentValidation.Extensions.Tests.Validators.Net;

public class IpValidatorsExtensionsTests
{
    // Hilfsklasse für die Tests
    private class TestModel
    {
        public string? IpAddress { get; set; }
    }

    // --- Tests für IPv4 ---

    [Theory]
    [InlineData("192.168.0.1")]
    [InlineData("127.0.0.1")]
    [InlineData("255.255.255.255")]
    [InlineData("0.0.0.0")]
    [InlineData("192.168.1")]
    [InlineData("")]                  // Leerstring
    [InlineData("1")]                  // Leerstring
    [InlineData("1.1")]                  // Leerstring
    public void IsIpv4Address_Should_NotHaveError_When_IpIsValid(string validIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpv4Address();
        var model = new TestModel { IpAddress = validIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IpAddress);
    }

    [Theory]
    [InlineData("256.0.0.1")]        // Zahlen zu gr
    [InlineData("192.168.1.1.1")]     // Zu lang
    [InlineData("abc.def.ghi.jkl")]   // Keine Zahlen
    [InlineData("::1")]               // IPv6 (sollte hier failen)
    public void IsIpv4Address_Should_HaveError_When_IpIsInvalid(string? invalidIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpv4Address();
        var model = new TestModel { IpAddress = invalidIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IpAddress);
    }

    // --- Tests für IPv6 ---

    [Theory]
    [InlineData("::1")]
    [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [InlineData("2001:db8::1")]
    [InlineData("fe80::1ff:fe23:4567:890a")]
    public void IsIpv6Address_Should_NotHaveError_When_IpIsValid(string validIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpv6Address();
        var model = new TestModel { IpAddress = validIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IpAddress);
    }

    [Theory]
    [InlineData("192.168.0.1")]       // IPv4 (sollte hier failen)
    [InlineData("12345::")]           // Segment zu groß (hex)
    [InlineData("zz::")]              // Kein Hex
    [InlineData(":")]                 // Zu kurz
    public void IsIpv6Address_Should_HaveError_When_IpIsInvalid(string invalidIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpv6Address();
        var model = new TestModel { IpAddress = invalidIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IpAddress);
    }

    // --- Tests für generische IP (v4 ODER v6) ---

    [Theory]
    [InlineData("192.168.1.1")] // Valid v4
    [InlineData("::1")]         // Valid v6
    public void IsIpAddress_Should_NotHaveError_When_IpIsValidV4orV6(string validIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpAddress();
        var model = new TestModel { IpAddress = validIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IpAddress);
    }

    [Theory]
    [InlineData("999.999.999.999")]
    [InlineData("NoIpAddress")]
    public void IsIpAddress_Should_HaveError_When_InputIsGarbage(string invalidIp)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.IpAddress).IsIpAddress();
        var model = new TestModel { IpAddress = invalidIp };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IpAddress);
    }
}