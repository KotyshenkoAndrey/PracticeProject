using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Web.Cars.Models;

public class CreateCarViewModel
{
    public Guid SellerId { get; set; }

    [Required(ErrorMessage = "Model is required")]
    [StringLength(50, ErrorMessage = "Model must be no more than 50 characters")]
    public string Model { get; set; }

    [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
    public int? Year { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Description must be no more than 500 characters")]
    public string Description { get; set; }

    [StringLength(20, ErrorMessage = "Color must be no more than 20 characters")]
    public string Color { get; set; }
}
