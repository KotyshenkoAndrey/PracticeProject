namespace PracticeProject.Web.Cars.Models;

public class CreateCarViewModel
{
    public Guid SellerId { get; set; }
    public string Model { get; set; }
    public int? Year { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
}
