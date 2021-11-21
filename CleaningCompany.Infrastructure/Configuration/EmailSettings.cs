namespace CleaningCompany.Infrastructure.Configuration
{
    public class EmailSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }
}
