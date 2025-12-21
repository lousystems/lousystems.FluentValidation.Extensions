using FluentValidation.Extensions.Validators.Security;
using FluentValidation.TestHelper;

namespace FluentValidation.Extensions.Tests.Validators.Net;

public class JwtValidatorsTests
{
    // Helper model for testing
    private class TestModel
    {
        public string? Token { get; set; }
    }

    // ==========================================
    // 1. Valid Token Tests
    // ==========================================

    [Theory]
    [InlineData("header.payload.signature")] // Minimal dummy valid
    [InlineData("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")] // Real JWT example
    [InlineData("a.b.c")] // Shortest technically valid version based on your logic
    public void IsJwtToken_Should_NotHaveError_When_FormatIsCorrect(string validToken)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Token).IsJwtToken();
        var model = new TestModel { Token = validToken };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Token);
    }

    // ==========================================
    // 2. Invalid Structure Tests
    // ==========================================

    [Theory]
    // Wrong number of parts
    [InlineData("header.payload")]              // Only 2 parts
    [InlineData("header.payload.signature.ex")] // 4 parts
    [InlineData("justastring")]                 // No dots (1 part)
    // Empty parts (Edge cases for Split)
    [InlineData("header..signature")]           // Middle part empty
    [InlineData(".payload.signature")]          // First part empty
    [InlineData("header.payload.")]             // Last part empty
    [InlineData("..")]                          // All parts empty
    public void IsJwtToken_Should_HaveError_When_FormatIsInvalid(string invalidToken)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Token).IsJwtToken();
        var model = new TestModel { Token = invalidToken };

        // Act
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Token)
            .WithErrorMessage("'Token' must be a valid JWT token.");
    }

    // ==========================================
    // 3. Null/Empty Tests (Optional Logic)
    // ==========================================

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsJwtToken_Should_NotHaveError_When_ValueIsEmpty(string? emptyToken)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Token).IsJwtToken();
        var model = new TestModel { Token = emptyToken };

        // Act
        // Your code says: if (string.IsNullOrWhiteSpace(value)) return true;
        // So these should PASS validation.
        var result = validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Token);
    }
    
    [Fact]
    public void IsJwtToken_Should_Fail_If_Combined_With_NotEmpty()
    {
        // Arrange: Verify that we can enforce presence if we chain NotEmpty()
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Token).NotEmpty().IsJwtToken();
        var model = new TestModel { Token = "" };

        // Act
        var result = validator.TestValidate(model);

        // Assert: Should fail because of NotEmpty, even though IsJwtToken returns true
        result.ShouldHaveValidationErrorFor(x => x.Token);
    }
}