using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;


namespace PracticeProject.Services.Cars.Models;

    public class CarViewModel
    {
    /// <summary>
    /// Car Guid
    /// </summary>
    public Guid CarId { get; set; }

    /// <summary>
    /// Car model
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Year of the car
    /// </summary>
    public int? Year { get; set; }

    /// <summary>
    /// Price of the car
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Description of the car
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Color of the car
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Seller Guid
    /// </summary>
    public Guid SellerId { get; set; }

    /// <summary>
    /// Full name of the seller
    /// </summary>
    public string SellerFullName { get; set; }

    /// <summary>
    /// Date when the car was posted
    /// </summary>
    public DateTime DatePosted { get; set; }

    /// <summary>
    /// Collection of viewing requests for the car
    /// </summary>
    public virtual ICollection<string>? ViewingRequestsCar { get; set; }
}

public class CarViewModelProfile : Profile
{
    public CarViewModelProfile()
    {
        CreateMap<Car, CarViewModel>()
            .BeforeMap<CarViewModelActions>()
            .ForMember(dest => dest.SellerId, opt => opt.Ignore());
    }

    public class CarViewModelActions : IMappingAction<Car, CarViewModel>
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;

        public CarViewModelActions(IDbContextFactory<MainDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public void Process(Car source, CarViewModel destination, ResolutionContext context)
        {
            using var db = contextFactory.CreateDbContext();

            var car = db.Cars.Include(x => x.Seller).ThenInclude(x => x.ViewingRequestsUser).FirstOrDefault(x => x.Id == source.Id);
            destination.CarId = car.Uid;
            destination.SellerId = car.Seller.Uid;
            destination.SellerFullName = car.Seller.FullName;
            destination.Model = car.Model;
            destination.Year = car.Year;
            destination.Price = car.Price;
            destination.Description = car.Description;
            destination.Color = car.Color;
            destination.DatePosted = car.DatePosted;
            destination.ViewingRequestsCar = car.ViewingRequestsCar?.Select(x => x.Sender.FullName).ToList();
        }
    }
}

