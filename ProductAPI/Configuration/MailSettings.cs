namespace SeminarAPI.Configuration
{
    public class MailSettings
    {
        public string? DisplayName { get; set; }
        public string? From { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
    }

    public class MailAssistantSettings
    {
        public string? DisplayNameAssistance { get; set; }
        public string? FromAssistance { get; set; }
        public string? UserNameAssistance { get; set; }
        public string? PasswordAssistance { get; set; }
        public string? HostAssistance { get; set; }
        public int PortAssistance { get; set; }
        public bool UseSSLAssistance { get; set; }
        public bool UseStartTlsAssistance { get; set; }
    }
}
