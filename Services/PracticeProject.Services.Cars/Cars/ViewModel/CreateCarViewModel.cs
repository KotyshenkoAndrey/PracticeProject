using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars.Models;



public class CreateCarViewModel
{
    public Guid SellerId { get; set; }
    public string Model { get; set; }
    public int? Year { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
}

public class CreateCarViewModelProfile : Profile
{
    public CreateCarViewModelProfile()
    {
        CreateMap<CreateCarViewModel, Car>()
            .ForMember(dest => dest.SellerId, opt => opt.Ignore())
            .AfterMap<CreateCarViewModelActions>();
    }

    public class CreateCarViewModelActions : IMappingAction<CreateCarViewModel, Car>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateCarViewModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateCarViewModel source, Car destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var seller = db.Sellers.FirstOrDefault(x => x.Uid == source.SellerId);
            destination.SellerId = seller.Id;
            destination.DatePosted = DateTime.Now;
        }
    }
}
public class CreateCarModelValidator : AbstractValidator<CreateCarViewModel>
{
    public CreateCarModelValidator(IDbContextFactory<MainDbContext> contextFactory)
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