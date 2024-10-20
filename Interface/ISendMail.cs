using ChatAPI.Services.SendMail;

namespace ChatAPI.Interface
{
    public interface ISendMail
    {
        void SetConfigFilePath(string configFilePath);
        void Send(string toAddress, string subject, string body);
    }
}