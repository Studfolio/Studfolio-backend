namespace Studfolio.AuthenticationService.Token
{
    /// <summary>
    /// Token configuration class filled in Startup.cs.
    /// </summary>
    public class TokenSettings
    {
        public double TokenLifetimeInMinutes { get; set; }
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
    }
}