using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;

namespace PracticeProject.Services.Sellers;

    public class SellerViewModel
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

public class SellerViewModelProfile : Profile
{
    public SellerViewModelProfile()
    {
        CreateMap<Seller, SellerViewModel>()
            .BeforeMap<SellerViewModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class SellerViewModelActions : IMappingAction<Seller, SellerViewModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public SellerViewModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Seller source, SellerViewModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var seller = db.Sellers.FirstOrDefault(x => x.Id == source.Id);
            destination.Id = seller.Uid;
            destination.Email = seller.Email;
            destination.Username = seller.Username;
            destination.FullName = seller.FullName;
            destination.PhoneNumber = seller.PhoneNumber;
        }
    }
}

