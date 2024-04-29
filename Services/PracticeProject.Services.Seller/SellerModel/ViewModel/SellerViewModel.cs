using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;

namespace PracticeProject.Services.Sellers;

    public class SellerViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
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

