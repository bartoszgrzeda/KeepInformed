using KeepInformed.Common.MailMessage;

namespace KeepInformed.Infrastructure.MailMessage;

public class MailMessageBuilder : IMailMessageBuilder
{
    private System.Net.Mail.MailMessage _mailMessage;

    public IMailMessageBuilder AddBody(string body)
    {
        _mailMessage.Body = body;

        return this;
    }

    public IMailMessageBuilder AddReceiver(string receiver)
    {
        _mailMessage.To.Add(receiver);

        return this;
    }

    public IMailMessageBuilder AddSubject(string subject)
    {
        _mailMessage.Subject = subject;

        return this;
    }

    public System.Net.Mail.MailMessage Build()
    {
        return _mailMessage;
    }

    public IMailMessageBuilder Create()
    {
        _mailMessage = new System.Net.Mail.MailMessage();

        return this;
    }
}
