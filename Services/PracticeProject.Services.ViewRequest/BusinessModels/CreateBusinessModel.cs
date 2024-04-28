using PracticeProject.Context.Entities;

namespace PracticeProject.Services.ViewingRequests.Models;



public class CreateBusinessModel
{
    public int CarId { get; set; }
    public int SenderId { get; set; }
    public StatusConfirm StateConfirmed { get; set; }
    public DateTime LastModifedDate { get; set; }
}

