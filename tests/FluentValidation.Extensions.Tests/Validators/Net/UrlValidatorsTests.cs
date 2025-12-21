using FluentValidation.Extensions.Validators.Net;
using FluentValidation.TestHelper;

namespace FluentValidation.Extensions.Tests.Validators.Net;

public class UrlValidatorsTests
{
    // A simple DTO to use for testing the validators
    private class TestModel
    {
        public string? WebsiteUrl { get; set; }
    }

    // ==========================================
    // 1. Tests for IsAbsoluteUrl
    // ==========================================

    [Theory]
    [InlineData("https://www.google.com")]
    [InlineData("http://localhost:5000")]
    [InlineData("ftp://fileserver.com/resource")]
    [InlineData("https://subdomain.example.co.uk/path?query=1")]
    public void IsAbsoluteUrl_Should_NotHaveError_When_UrlIsAbsolute(string validUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsAbsoluteUrl();
        var model = new TestModel { WebsiteUrl = validUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.WebsiteUrl);
    }

    [Theory]
    [InlineData("/relative/path")]
    [InlineData("images/logo.png")]
    [InlineData("www.google.com")] // Technically invalid absolute URL (missing scheme like https://)
    [InlineData("just-some-text")]
    public void IsAbsoluteUrl_Should_HaveError_When_UrlIsNotAbsolute(string invalidUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsAbsoluteUrl();
        var model = new TestModel { WebsiteUrl = invalidUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
    }

    // ==========================================
    // 2. Tests for IsRelativeUrl
    // ==========================================

    [Theory]
    [InlineData("/api/users")]
    [InlineData("assets/css/style.css")]
    [InlineData("../parent/folder")]
    [InlineData("./current/folder")]
    [InlineData("")]
    public void IsRelativeUrl_Should_NotHaveError_When_UrlIsRelative(string validRelativeUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsRelativeUrl();
        var model = new TestModel { WebsiteUrl = validRelativeUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.WebsiteUrl);
    }

    [Theory]
    [InlineData("https://www.google.com")] // Absolute, so it should fail relative check
    [InlineData("http://localhost")]
    [InlineData("ftp://test")]
    public void IsRelativeUrl_Should_HaveError_When_UrlIsAbsolute(string absoluteUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsRelativeUrl();
        var model = new TestModel { WebsiteUrl = absoluteUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
    }

    // ==========================================
    // 3. Tests for IsUrl (Generic)
    // ==========================================
    
    [Theory]
    [InlineData("https://www.microsoft.com")]
    [InlineData("http://127.0.0.1")]
    // If your "IsUrl" allows relative URLs, add them here. 
    // Usually, IsUrl checks for well-formed Absolute URIs by default in .NET.
    public void IsUrl_Should_NotHaveError_When_UrlIsValid(string validUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsUrl();
        var model = new TestModel { WebsiteUrl = validUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.WebsiteUrl);
    }

    [Theory]
    [InlineData("http:// very bad url with spaces")]
    public void IsUrl_Should_HaveError_When_UrlIsMalformed(string invalidUrl)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsUrl();
        var model = new TestModel { WebsiteUrl = invalidUrl };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
    }
    
    // ==========================================
    // 4. Null Checks (Standard FluentValidation Behavior)
    // ==========================================
    
    [Fact]
    public void AllValidators_Should_Skip_Null_Values()
    {
        // FluentValidation custom validators usually return valid for null 
        // (because you should use .NotNull() to enforce existence).
        
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.WebsiteUrl).IsAbsoluteUrl();
        
        var model = new TestModel { WebsiteUrl = null };
        var result = validator.TestValidate(model);

        result.ShouldNotHaveValidationErrorFor(x => x.WebsiteUrl);
    }
}