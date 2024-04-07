using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars;



public class CreateCarViewModel
{
    public Guid SellerId { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
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

            var seller = db.Users.FirstOrDefault(x => x.Uid == source.SellerId);

            destination.SellerId = seller.Id;
            destination.DatePosted = DateTime.Now;
        }
    }
}