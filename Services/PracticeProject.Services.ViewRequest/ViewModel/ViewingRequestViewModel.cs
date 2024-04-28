using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;


namespace PracticeProject.Services.ViewingRequests.Models;

    public class ViewingRequestViewModel
{
    public Guid RequestId { get; set; }
    public int CarId { get; set; }
    public int SenderId { get; set; }
    public StatusConfirm StateConfirmed { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? LastModifedDate { get; set; }
}

public class ViewingRequestViewModelProfile : Profile
{
    public ViewingRequestViewModelProfile()
    {
        CreateMap<ViewingRequest, ViewingRequestViewModel>()
            .BeforeMap<ViewingRequestViewModelActions>()
            .ForMember(dest => dest.RequestId, opt => opt.Ignore());
    }

    public class ViewingRequestViewModelActions : IMappingAction<ViewingRequest, ViewingRequestViewModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public ViewingRequestViewModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(ViewingRequest source, ViewingRequestViewModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var request = db.ViewingRequests.Include(x => x.Car).ThenInclude(x => x.Seller).FirstOrDefault(x => x.Id == source.Id);
            destination.RequestId = request.Uid;
            destination.CarId = request.Car.Id;
            destination.SenderId = request.Sender.Id;
            destination.StateConfirmed = request.StateConfirmed;
            destination.RequestDate = request.RequestDate;
            destination.LastModifedDate = request.LastModifedDate;
        }
    }
}

