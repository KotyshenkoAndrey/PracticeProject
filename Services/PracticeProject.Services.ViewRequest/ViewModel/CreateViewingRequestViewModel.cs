using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.ViewingRequests.Models;



public class CreateViewingRequestViewModel
{
    /// <summary>
    /// Guid car
    /// </summary>
    public Guid CarId { get; set; }
    /// <summary>
    /// Guid sender user
    /// </summary>
    public Guid SenderId { get; set; }
}

public class CreateViewingRequestProfile : Profile
{
    public CreateViewingRequestProfile()
    {
        CreateMap<CreateViewingRequestViewModel, ViewingRequest>()
            .ForMember(dest => dest.CarId, opt => opt.Ignore())
            .ForMember(dest => dest.SenderId, opt => opt.Ignore())
            .AfterMap<CreateViewingRequestActions>();
    }

    public class CreateViewingRequestActions : IMappingAction<CreateViewingRequestViewModel, ViewingRequest>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CreateViewingRequestActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(CreateViewingRequestViewModel source, ViewingRequest destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var car = db.Cars.FirstOrDefault(x => x.Uid == source.CarId);
            destination.CarId = car.Id;
            var sender = db.Sellers.FirstOrDefault(x => x.Uid == source.SenderId);
            destination.SenderId = sender.Id;
            destination.RequestDate = DateTime.Now;
            destination.StateConfirmed = StatusConfirm.Wait;
        }
    }
}
