using FluentValidation.Extensions.Validators.Net;
using FluentValidation.TestHelper;

namespace FluentValidation.Extensions.Tests.Validators.Net;
/*
public class IpAddressExtensionsTests
{
    private class TestModel
    {
        public string? IpV4 { get; set; }
        public string? IpV6 { get; set; }
    }
    
    private class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.IpV4).IsIpv4Address();
            RuleFor(x => x.IpV6).MustBeIPv6();
        }
    }

    private readonly TestValidator _validator = new();

    // --- TESTS for IPv4 ---

    [Theory]
    [InlineData("192.168.1.1")]
    [InlineData("127.0.0.1")]
    [InlineData("8.8.8.8")]
    [InlineData("0.0.0.0")]
    [InlineData("255.255.255.255")]
    [InlineData("192.168.1")]   
    [InlineData("1")]  
    public void MustBeIPv4_Should_Pass_For_Valid_IPs(string ip)
    {
        var model = new TestModel { IpV4 = ip };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.IpV4);
    }

    [Theory]
    [InlineData("256.256.256.256")] 
    [InlineData("192.168.1.1.1")]     
    [InlineData("abc.def.ghi.jkl")] 
    [InlineData("::1")]             
    public void MustBeIPv4_Should_Fail_For_Invalid_IPs(string ip)
    {
        var model = new TestModel { IpV4 = ip };
        
        var result = _validator.TestValidate(model);


        result.ShouldHaveValidationErrorFor(x => x.IpV4)
              .WithErrorMessage("'Ip V4' must be a valid IPv4 address (e.g. 192.168.1.1).");
    }

    [Fact]
    public void MustBeIPv4_Should_Pass_For_Null_Or_Empty()
    {
        
        var modelNull = new TestModel { IpV4 = null };
        _validator.TestValidate(modelNull).ShouldNotHaveValidationErrorFor(x => x.IpV4);

        var modelEmpty = new TestModel { IpV4 = "" };
        _validator.TestValidate(modelEmpty).ShouldNotHaveValidationErrorFor(x => x.IpV4);
    }
    

    [Theory]
    [InlineData("::1")]
    [InlineData("2001:0db8:85a3:0000:0000:8a2e:0370:7334")]
    [InlineData("2001:db8::1")] 
    [InlineData("fe80::1ff:fe23:4567:890a")] 
    public void MustBeIPv6_Should_Pass_For_Valid_IPs(string ip)
    {
        var model = new TestModel { IpV6 = ip };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.IpV6);
    }

    [Theory]
    [InlineData("192.168.1.1")]     
    [InlineData("gggg::1")]         
    [InlineData("12345::")]         
    [InlineData(":1:")]             
    public void MustBeIPv6_Should_Fail_For_Invalid_IPs(string ip)
    {
        var model = new TestModel { IpV6 = ip };
        
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.IpV6)
              .WithErrorMessage("'Ip V6' must be a valid IPv6 address (e.g. 2001:0db8:85a3:0000:0000:8a2e:0370:7334).");
    }
}*/