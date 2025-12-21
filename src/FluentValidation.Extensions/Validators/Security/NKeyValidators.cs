using FluentValidation.Extensions.Validators.Security.NKey;

namespace FluentValidation.Extensions.Validators.Security;

public static class NKeyValidators
{
    
    extension<T>(IRuleBuilder<T, string?> ruleBuilder)
    {
        

        /// <summary>
        /// Validates that the property is ANY valid NKey Public Key (U, A, O, N, C).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKey(NKeyType type = NKeyType.AnyPublicKey)
            => ruleBuilder.SetValidator(new NKeyValidator<T>(type));
    }



}