using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;

namespace PracticeProject.Services.Sellers;

public class SellerProfileModel
{
    /// <summary>
    /// User's email
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

public class SellerProfileModelProfile : Profile
{
    public SellerProfileModelProfile()
    {
        CreateMap<Seller, SellerProfileModel>()
            .BeforeMap<SellerProfileModelActions>();
    }

    public class SellerProfileModelActions : IMappingAction<Seller, SellerProfileModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public SellerProfileModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Seller source, SellerProfileModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var seller = db.Sellers.FirstOrDefault(x => x.Id == source.Id);
            destination.Email = seller.Email;
            destination.FullName = seller.FullName;
            destination.PhoneNumber = seller.PhoneNumber;
        }
    }
}

