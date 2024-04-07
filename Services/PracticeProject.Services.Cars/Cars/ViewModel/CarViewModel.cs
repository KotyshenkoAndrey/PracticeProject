using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Context.Entities;
using PracticeProject.Context;
using System;
using System.Collections.Generic;
namespace PracticeProject.Services.Cars;

    public class CarViewModel
    {
        public string Model { get; set; }
        public int? Year { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public Guid SellerId { get; set; }
        public string SellerFullName { get; set; }
        public DateTime DatePosted { get; set; }
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
            destination.SellerId = car.Seller.Uid;
            destination.SellerFullName = car.Seller.FullName;
            destination.Model = car.Model;
            destination.Year = car.Year;
            destination.Price = car.Price;
            destination.Description = car.Description;
            destination.Color = car.Color;
            destination.DatePosted = car.DatePosted;
            destination.ViewingRequestsCar = car.ViewingRequestsCar?.Select(x => x.Seller.FullName).ToList();
        }
    }
}

