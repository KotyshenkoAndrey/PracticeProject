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

    public async Task PublicateNewCar(CarSendModel model)
    {
        await rabbitMq.PushAsync(QueueNames.PUBLICATE_NEW_CAR, model);
    }
}
