namespace FluentValidation.Extensions.Validators.Net;

public static class IpValidators
{
    extension<T>(IRuleBuilder<T, string> ruleBuilder)
    {
        public IRuleBuilderOptions<T, string?> IsIpv4Address()
            => ruleBuilder.SetValidator(new Ipv4Validator<T>());
        
        public IRuleBuilderOptions<T, string?> IsIpv6Address()
            => ruleBuilder.SetValidator(new Ipv6Validator<T>());
        
        public IRuleBuilderOptions<T, string?> IsIpAddress() 
            => ruleBuilder.SetValidator(new IpAddressValidator<T>());
    }
}

