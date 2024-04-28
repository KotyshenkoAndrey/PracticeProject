using PracticeProject.Context.Entities;
using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Services.ViewingRequests.Models;

public class UpdateBusinessModel
{
    public int CarId { get; set; }
    public int SenderId { get; set; }
    public StatusConfirm StateConfirmed { get; set; }
    public DateTime LastModifedDate { get; set; }
}
