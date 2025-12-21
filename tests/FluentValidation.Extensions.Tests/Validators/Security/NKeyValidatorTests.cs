using FluentValidation.Extensions.Validators.Security;
using FluentValidation.Extensions.Validators.Security.NKey;
using FluentValidation.TestHelper;

namespace FluentValidation.Extensions.Tests.Validators.Security;

public class NKeyValidatorTests
{
    private class TestModel
    {
        public string? Key { get; set; }
    }

    // Helper to generate a valid string of specific length
    // 'A' is valid in Base32 (A-Z, 0-9)
    private static string CreateKey(string prefix, int totalLength) 
        => prefix + new string('A', totalLength - prefix.Length);

    // ==========================================
    // 1. Seed Tests (Length 58)
    // ==========================================

    [Theory]
    [InlineData(NKeyType.UserSeed, "SU")]
    [InlineData(NKeyType.AccountSeed, "SA")]
    [InlineData(NKeyType.OperatorSeed, "SO")]
    [InlineData(NKeyType.ServerSeed, "SN")]
    [InlineData(NKeyType.ClusterSeed, "SC")]
    public void IsNKey_Should_Validate_Specific_Seeds(NKeyType type, string prefix)
    {
        // Arrange
        var validSeed = CreateKey(prefix, 58); // Seeds are 58 chars
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(type);
        
        // Act
        var result = validator.TestValidate(new TestModel { Key = validSeed });

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Key);
    }

    [Fact]
    public void IsNKey_AnySeed_Should_Validate_All_Seed_Types()
    {
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(NKeyType.AnySeed);

        // Test all valid seed prefixes
        var prefixes = new[] { "SU", "SA", "SO", "SN", "SC" };

        foreach (var prefix in prefixes)
        {
            var seed = CreateKey(prefix, 58);
            var result = validator.TestValidate(new TestModel { Key = seed });
            result.ShouldNotHaveValidationErrorFor(x => x.Key);
        }
    }

    // ==========================================
    // 2. Public Key Tests (Length 56)
    // ==========================================

    [Theory]
    [InlineData(NKeyType.UserPublic, "U")]
    [InlineData(NKeyType.AccountPublic, "A")]
    // Based on your provided Regex: OperatorPublic starts with 'N'
    [InlineData(NKeyType.OperatorPublic, "N")] 
    // Based on your provided Regex: ServerPublic starts with 'O'
    [InlineData(NKeyType.ServerPublic, "O")] 
    [InlineData(NKeyType.ClusterPublic, "C")]
    public void IsNKey_Should_Validate_Specific_PublicKeys(NKeyType type, string prefix)
    {
        // Arrange
        var validKey = CreateKey(prefix, 56); // Public keys are 56 chars
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(type);
        
        // Act
        var result = validator.TestValidate(new TestModel { Key = validKey });

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Key);
    }
    
    [Fact]
    public void IsNKey_AnyPublicKey_Should_Validate_All_Public_Types()
    {
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(NKeyType.AnyPublicKey);

        // Prefixes based on your regexes
        var prefixes = new[] { "U", "A", "N", "O", "C" };

        foreach (var prefix in prefixes)
        {
            var key = CreateKey(prefix, 56);
            var result = validator.TestValidate(new TestModel { Key = key });
            result.ShouldNotHaveValidationErrorFor(x => x.Key);
        }
    }

    // ==========================================
    // 3. Invalid Format Tests
    // ==========================================

    [Theory]
    [InlineData(NKeyType.UserSeed, "SU", 57)] // Too short
    [InlineData(NKeyType.UserSeed, "SU", 59)] // Too long
    [InlineData(NKeyType.UserPublic, "U", 55)] // Too short
    [InlineData(NKeyType.UserPublic, "U", 57)] // Too long
    public void IsNKey_Should_Fail_When_Length_Is_Incorrect(NKeyType type, string prefix, int length)
    {
        // Arrange
        var invalidLengthKey = CreateKey(prefix, length);
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(type);

        // Act
        var result = validator.TestValidate(new TestModel { Key = invalidLengthKey });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }

    [Theory]
    [InlineData(NKeyType.UserSeed, "SA")] // Account prefix for User validator
    [InlineData(NKeyType.AccountSeed, "SU")]
    [InlineData(NKeyType.UserPublic, "A")]
    public void IsNKey_Should_Fail_When_Prefix_Does_Not_Match_Type(NKeyType type, string wrongPrefix)
    {
        // Arrange
        var wrongKey = CreateKey(wrongPrefix, 58); // Assuming seed length for test simplicity
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(type);

        // Act
        var result = validator.TestValidate(new TestModel { Key = wrongKey });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }

    [Fact]
    public void IsNKey_Should_Fail_When_Characters_Are_Invalid()
    {
        // Arrange
        // Lowercase is invalid in NKeys (they are uppercase base32-ish)
        var invalidChars = "su" + new string('a', 56); 
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(NKeyType.UserSeed);

        // Act
        var result = validator.TestValidate(new TestModel { Key = invalidChars });

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Key);
    }

    // ==========================================
    // 4. Null/Empty Tests
    // ==========================================

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void IsNKey_Should_Pass_When_Value_Is_Empty(string? emptyValue)
    {
        // Arrange
        var validator = new InlineValidator<TestModel>();
        validator.RuleFor(x => x.Key).IsNKey(NKeyType.UserSeed);

        // Act
        var result = validator.TestValidate(new TestModel { Key = emptyValue });

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Key);
    }
}