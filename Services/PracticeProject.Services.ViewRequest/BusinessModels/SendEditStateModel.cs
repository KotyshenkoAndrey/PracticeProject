using PracticeProject.Context.Entities;

namespace PracticeProject.Services.ViewRequest.BusinessModels
{
    public class SendEditStateModel
    {
        public Guid idRequest { get; set; }
        public StatusConfirm state { get; set; }
    }
}
