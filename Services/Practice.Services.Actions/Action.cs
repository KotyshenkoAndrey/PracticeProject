namespace PracticeProject.Services.Actions;

using PracticeProject.Services.RabbitMq;
using System.Threading.Tasks;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendMail(EmailSendModel model)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_MAIL, model);
    }
}
