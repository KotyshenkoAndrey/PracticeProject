using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;


namespace PracticeProject.Services.ViewingRequests.Models;

    public class ViewingRequestViewModel
{
    /// <summary>
    /// Unique identifier of the request
    /// </summary>
    public Guid RequestId { get; set; }

    /// <summary>
    /// Identifier of the car
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Car model
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Year of the car
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Identifier of the sender
    /// </summary>
    public int SenderId { get; set; }

    /// <summary>
    /// Full name of the sender
    /// </summary>
    public string SenderFullName { get; set; }

    /// <summary>
    /// Full name of the seller
    /// </summary>
    public string SellerFullName { get; set; }

    /// <summary>
    /// Confirmation status of the request
    /// </summary>
    public StatusConfirm StateConfirmed { get; set; }

    /// <summary>
    /// Date of the request
    /// </summary>
    public DateTime RequestDate { get; set; }

    /// <summary>
    /// Date of the last modification
    /// </summary>
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
            destination.Model = request.Car.Model;
            destination.Year = request.Car.Year ?? 0;
            destination.StateConfirmed = request.StateConfirmed;
            destination.RequestDate = request.RequestDate;
            destination.LastModifedDate = request.LastModifedDate;
            destination.SellerFullName = request.Car.Seller.FullName;
            destination.SenderFullName = request.Sender.FullName;
        }
    }
}

