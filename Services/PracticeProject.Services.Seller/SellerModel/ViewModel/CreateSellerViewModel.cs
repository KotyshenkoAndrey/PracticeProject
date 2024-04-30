using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Sellers;



public class CreateSellerViewModel
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User's username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// User's full name
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// User's phone number
    /// </summary>
    public string? PhoneNumber { get; set; }
}

public class CreateSellerViewModelProfile : Profile
{
    public CreateSellerViewModelProfile()
    {
        CreateMap<Seller, CreateSellerViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Uid))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            ;
    }  
}
public class CreateSellerModelValidator : AbstractValidator<CreateSellerViewModel>
{
    public CreateSellerModelValidator(IDbContextFactory<MainDbContext> contextFactory)
    {

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .Must((Username) =>
            {
                using var context = contextFactory.CreateDbContext();
                var found = context.Sellers.Any(a => a.Username == Username);
                return !found;
            }).WithMessage("Username is already in use");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .MinimumLength(1).WithMessage("Minimum length: 1")
            .MaximumLength(50).WithMessage("Maximum length: 50")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email address format");

        RuleFor(x => x.FullName)
            .MinimumLength(1).WithMessage("Minimum length: 1")
            .MaximumLength(50).WithMessage("Maximum length: 50");

        RuleFor(x => x.PhoneNumber)
            .MinimumLength(1).WithMessage("Minimum length: 1")
            .MaximumLength(12).WithMessage("Maximum length: 50")
            .Matches(@"^\+[0-9]{1,11}$").WithMessage("Phone number must start with a '+' and contain only numbers after that")
            .Matches(@"^[0-9+]*$").WithMessage("Phone number can only contain numbers and a single '+' at the beginning")
            ;
    }
}