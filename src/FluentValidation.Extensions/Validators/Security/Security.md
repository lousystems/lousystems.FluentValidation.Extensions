For JWT scroll down

# Security & Identity Validation

This module provides validators for specific authentication and identity formats, including NATS NKeys and JSON Web Tokens (JWT).

### NATS NKey Validation

Validates [NATS 2.0+ NKeys](https://docs.nats.io/nats-concepts/security/nkeys), ensuring correct prefixes (e.g., `U` for User, `O` for Operator) and correct lengths for both Seeds (private) and Public Keys.

#### Usage

```csharp
public class NatsConfigValidator : AbstractValidator<NatsConfig>
{
    public NatsConfigValidator()
    {
        // Validates a User Seed (starts with 'SU', length 58)
        RuleFor(x => x.UserSeed).IsNKey(NKeyType.UserSeed);

        // Validates a Cluster Public Key (starts with 'C', length 56)
        RuleFor(x => x.ClusterId).IsNKey(NKeyType.ClusterPublic);
        
        // Validates any type of Seed (starts with 'S')
        RuleFor(x => x.GenericSecret).IsNKey(NKeyType.AnySeed);
    }
}
```

#### Technical Behavior

* **Strict Pattern Matching:** Uses generated `Regex` patterns to enforce the exact prefix and length requirements defined by the NATS specification (Base32 characters).
* **Type Safety:** The `NKeyType` enum prevents mismatch errors (e.g., validating a Public Key field against a Seed pattern).
* **Performance:** Utilizes .NET `[GeneratedRegex]` (source generators) where possible for optimized matching speed.
* **Null Handling:** Ignores `null` or empty values (returns valid). Combine with `.NotEmpty()` for mandatory keys.

---

### JWT Validation

Performs a structural validation of JSON Web Tokens (JWT) to ensure they are well-formed before attempting expensive cryptographic verification.

#### Usage

```csharp
public class AuthValidator : AbstractValidator<AuthRequest>
{
    public AuthValidator()
    {
        // Checks for "Header.Payload.Signature" structure
        RuleFor(x => x.AccessToken).IsJwtToken();
        
        // Typical usage with mandatory check
        RuleFor(x => x.IdToken).NotEmpty().IsJwtToken();
    }
}
```

#### Technical Behavior

* **Structural Integrity:** Validates that the token string consists of exactly three parts (Header, Payload, Signature) separated by dots (`.`).
* **Content Check:** Ensures individual parts are not empty or whitespace.
* **Scope:** This validator checks **format only**. It does *not* verify the cryptographic signature or decode the Base64Url payload. This is intended as a fast "fail-early" check before passing the token to a verifier like `System.IdentityModel.Tokens.Jwt`.
* **Null Handling:** Returns `true` for `null` or empty strings.