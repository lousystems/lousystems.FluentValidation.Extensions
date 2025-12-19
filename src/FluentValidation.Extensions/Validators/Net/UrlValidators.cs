namespace FluentValidation.Extensions.Validators.Net;

public static class UrlValidators
{
    extension<T>(IRuleBuilder<T, string> ruleBuilder)
    {
        public IRuleBuilderOptions<T, string?> IsAbsoluteUrl()
            => ruleBuilder.SetValidator(new AbsoluteUrlValidator<T>());
        
        public IRuleBuilderOptions<T, string?> IsRelativeUrl()
            => ruleBuilder.SetValidator(new RelativeUrlRegexValidator<T>());
        
        public  IRuleBuilderOptions<T, string?> IsUrl() 
            => ruleBuilder.SetValidator(new UrlValidator<T>());
        
    }

}