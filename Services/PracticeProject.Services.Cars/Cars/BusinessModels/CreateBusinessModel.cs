namespace PracticeProject.Services.Cars.Models;



public class CreateBusinessModel
{
    public Guid UserId { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public DateTime DatePosted { get; set; }
}

