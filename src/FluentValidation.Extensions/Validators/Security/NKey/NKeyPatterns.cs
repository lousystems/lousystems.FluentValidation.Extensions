using System.Text.RegularExpressions;

namespace FluentValidation.Extensions.Validators.Security.NKey;

public static partial class NKeyPatterns
{
    // --- SEEDS (58 chars) ---
    
    [GeneratedRegex(@"^SU[A-Z0-9]{56}$")]
    public static partial Regex UserSeed();

    [GeneratedRegex(@"^SA[A-Z0-9]{56}$")]
    public static partial Regex AccountSeed();

    [GeneratedRegex(@"^SO[A-Z0-9]{56}$")]
    public static partial Regex OperatorSeed();
    
    [GeneratedRegex(@"^SN[A-Z0-9]{56}$")]
    public static partial Regex ServerSeed();
    
    [GeneratedRegex(@"^SC[A-Z0-9]{56}$")]
    public static partial Regex ClusterSeed();
    
    [GeneratedRegex(@"^S[UAONC][A-Z0-9]{56}$")]
    public static partial Regex AnySeed();

    // --- PUBLIC KEYS (56 chars) ---

    [GeneratedRegex(@"^U[A-Z0-9]{55}$")]
    public static partial Regex UserPublic();

    [GeneratedRegex(@"^A[A-Z0-9]{55}$")]
    public static partial Regex AccountPublic();
    
    [GeneratedRegex(@"^N[A-Z0-9]{55}$")]
    public static partial Regex OperatorPublic();

    [GeneratedRegex(@"^O[A-Z0-9]{55}$")]
    public static partial Regex ServerPublic();
    
    [GeneratedRegex(@"^C[A-Z0-9]{55}$")]
    public static partial Regex ClusterPublic();
    
    [GeneratedRegex(@"^[OAUNC][A-Z0-9]{55}$")]
    public static partial Regex AnyPublicKey();

    
}