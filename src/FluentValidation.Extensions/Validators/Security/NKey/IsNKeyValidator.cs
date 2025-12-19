using FluentValidation.Validators;

namespace FluentValidation.Extensions.Validators.Security.NKey;

public class NKeyValidator<T>(NKeyType type) : PropertyValidator<T, string?>
{
    public override string Name => "IsNKeySeed";
    

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return true;
        return type switch
        {
            NKeyType.UserSeed => NKeyPatterns.UserSeed().IsMatch(value),
            NKeyType.AccountSeed => NKeyPatterns.AccountSeed().IsMatch(value),
            NKeyType.OperatorSeed => NKeyPatterns.OperatorSeed().IsMatch(value),
            NKeyType.ServerSeed => NKeyPatterns.ServerSeed().IsMatch(value),
            NKeyType.ClusterSeed => NKeyPatterns.ClusterSeed().IsMatch(value),
            NKeyType.AnySeed => NKeyPatterns.AnySeed().IsMatch(value),
            
            NKeyType.UserPublic => NKeyPatterns.UserPublic().IsMatch(value),
            NKeyType.AccountPublic => NKeyPatterns.AccountPublic().IsMatch(value),
            NKeyType.OperatorPublic => NKeyPatterns.OperatorPublic().IsMatch(value),
            NKeyType.AnyPublicKey => NKeyPatterns.AnyPublicKey().IsMatch(value),
            NKeyType.ServerPublic => NKeyPatterns.ServerPublic().IsMatch(value),
            NKeyType.ClusterPublic => NKeyPatterns.ClusterPublic().IsMatch(value),
            _ => false
        };
        
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must be a valid NKey seed.";
}