using AutoMapper;
using FluentValidation;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars.Models;

public class UpdateCarViewModel
{
    /// <summary>
    /// Car model
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Price of the car
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Year of the car
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Description of the car
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Color of the car
    /// </summary>
    public string? Color { get; set; }
}
public class UpdateCarViewModelProfile : Profile
{
    public UpdateCarViewModelProfile()
    {
        CreateMap<UpdateCarViewModel, Car>();
    }
}

public class UpdateCarViewModelValidator : AbstractValidator<UpdateCarViewModel>
{
    public UpdateCarViewModelValidator()
    {
        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required")
            .MinimumLength(1).WithMessage("Minimum length: 1")
            .MaximumLength(50).WithMessage("Maximum length: 50");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, 2100).When(x => x.Year != null).WithMessage("Year should be between 1900 and 2100");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price should be greater than 0");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Maximum length: 500");

        RuleFor(x => x.Color)
            .MaximumLength(20).WithMessage("Maximum length: 20");
    }
}