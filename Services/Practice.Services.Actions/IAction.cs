namespace PracticeProject.Services.Actions;

using System.Threading.Tasks;

public interface IAction
{
    Task PublicateNewCar(CarSendModel model);
}
