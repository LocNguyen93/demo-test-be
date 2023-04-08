namespace RF.Web.Api.AppSettings
{
    public class Authentication
    {
        public int TokenLifespanMinutes { get; set; }
        public string DataProtectionKeysLocation { get; set; }

        public JwtTokenOptions JwtToken { get; set; }
        public AuthLockoutOptions Lockout { get; set; }
        public AuthPasswordOptions Password { get; set; }
    }

    public class AuthLockoutOptions
    {
        public int MaximumAttempt { get; set; }
        public int LockoutMinutes { get; set; }
    }

    public class AuthPasswordOptions
    {
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireUppercase { get; set; }
        public int RequiredLength { get; set; }
    }

    public class JwtTokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int LifeTimeMinutes { get; set; }
        public int RefreshTokenLifeTimeMinutes { get; set; }
    }

    public class InternalApi
    {
        public string[] ApiKeyPath { get; set; }
        public string ApiKeyHeader { get; set; }
        public string ApiKeyValue { get; set; }
    }
}
