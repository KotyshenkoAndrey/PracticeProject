namespace PracticeProject.Worker;

using PracticeProject.Services.RabbitMq;
using System.Threading.Tasks;
using PracticeProject.Services.Actions;
using PracticeProject.Services.Logger;
using System.Net.Mail;
using System.Net;
using PracticeProject.Worker.Settings;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;
    private readonly MailSettings mailSettings;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
        , MailSettings mailSettings
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
        this.mailSettings = mailSettings;
    }

    public void Start()
    {
        rabbitMq.Subscribe<EmailSendModel>(QueueNames.SEND_MAIL, async data =>
        {
            logger.Information($"Send mail::: {data.Receiver.Count}    {data.Receiver.ToString}");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailSettings.From);
            mail.To.Add(new MailAddress(data.Receiver[0]));
            for (int i = 1; i < data.Receiver.Count; ++i)
            {
                mail.CC.Add(new MailAddress(data.Receiver[i]));
            }
            mail.Subject = data.Subject;
            mail.Body = data.Body;

            SmtpClient client = new SmtpClient();
            client.Host = mailSettings.Host;
            client.Port = mailSettings.Port;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(mailSettings.Login, mailSettings.Password);
            client.Send(mail);
        });
    }
}