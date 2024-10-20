namespace ChatAPI.Entities
{
    public class SmtpConfig
    {
        public string smtpServer { get; set; }
        public int smtpPort { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool useTls { get; set; }
    }
}
