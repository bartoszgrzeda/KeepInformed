namespace KeepInformed.Common.MailMessage;

public interface IMailMessageSender
{
    Task Send(System.Net.Mail.MailMessage mailMessage);
}
