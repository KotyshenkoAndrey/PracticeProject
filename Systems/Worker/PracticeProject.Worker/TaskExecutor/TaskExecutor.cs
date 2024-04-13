namespace PracticeProject.Worker;

using PracticeProject.Services.RabbitMq;
using System.Threading.Tasks;
using PracticeProject.Services.Actions;
using PracticeProject.Services.Logger;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
    }

    public void Start()
    {
        rabbitMq.Subscribe<CarSendModel>(QueueNames.PUBLICATE_NEW_CAR, async data =>
        {
            logger.Information($"start metod::: {data.Id}");

            await Task.Delay(1000);

            logger.Information($"stop metod::: {data.Id} | {data.Model}");
        });
    }
}