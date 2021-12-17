namespace MaskedEmails.Services.Configuration
{
    public class AppSettings
    {
        public string DomainName { get; set; }
        public int PasswordLength { get; set; } = 25;
    }
}