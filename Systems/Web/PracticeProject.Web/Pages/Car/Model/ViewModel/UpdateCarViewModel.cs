namespace PracticeProject.Web.Cars.Models;

public class UpdateCarViewModel
{
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int? Year { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
}