namespace PracticeProject.Services.Actions;

using System.Threading.Tasks;

public interface IAction
{
    Task SendMail(EmailSendModel model);
}
