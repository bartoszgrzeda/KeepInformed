namespace KeepInformed.Common.MailMessage;

public interface IMailMessageBuilder
{
    IMailMessageBuilder Create();
    IMailMessageBuilder AddReceiver(string receiver);
    IMailMessageBuilder AddBody(string body);
    IMailMessageBuilder AddSubject(string subject);
    System.Net.Mail.MailMessage Build();
}
