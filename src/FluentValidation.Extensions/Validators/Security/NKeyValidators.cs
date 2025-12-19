using FluentValidation.Extensions.Validators.Security.NKey;

namespace FluentValidation.Extensions.Validators.Security;

public static class NKeyValidators
{
    
    extension<T>(IRuleBuilder<T, string?> ruleBuilder)
    {
        // --- SEEDS (Private Keys) ---
        
        /// <summary>
        /// Validates that the property is a valid NKey User Seed (starts with SU).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyUserSeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.UserSeed));

        /// <summary>
        /// Validates that the property is a valid NKey Account Seed (starts with SA).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyAccountSeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.AccountSeed));

        /// <summary>
        /// Validates that the property is a valid NKey Operator Seed (starts with SO).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyOperatorSeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.OperatorSeed));

        /// <summary>
        /// Validates that the property is a valid NKey Server Seed (starts with SN).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyServerSeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.ServerSeed));

        /// <summary>
        /// Validates that the property is a valid NKey Cluster Seed (starts with SC).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyClusterSeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.ClusterSeed));

        /// <summary>
        /// Validates that the property is ANY valid NKey Seed (SU, SA, SO, SN, SC).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeySeed()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.AnySeed));

        // --- PUBLIC KEYS (IDs) ---
        
        /// <summary>
        /// Validates that the property is a valid NKey User ID (starts with U).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyUserPublic()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.UserPublic));

        /// <summary>
        /// Validates that the property is a valid NKey Account ID (starts with A).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyAccountPublic()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.AccountPublic));

        /// <summary>
        /// Validates that the property is a valid NKey Operator ID (starts with O).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyOperatorPublic()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.OperatorPublic));

        /// <summary>
        /// Validates that the property is a valid NKey Server ID (starts with N).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyServerPublic()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.ServerPublic));

        /// <summary>
        /// Validates that the property is a valid NKey Cluster ID (starts with C).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKeyClusterPublic()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.ClusterPublic));

        /// <summary>
        /// Validates that the property is ANY valid NKey Public Key (U, A, O, N, C).
        /// </summary>
        public IRuleBuilderOptions<T, string?> IsNKey()
            => ruleBuilder.SetValidator(new NKeyValidator<T>(NKeyType.AnyPublicKey));
    }



}