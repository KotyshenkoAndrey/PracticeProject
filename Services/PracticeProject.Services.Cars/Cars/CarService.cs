using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context;
using PracticeProject.Context.Entities;
using PracticeProject.Services.Cars.Models;
using Microsoft.AspNetCore.SignalR;
using PracticeProject.Services.AppCarHub;

namespace PracticeProject.Services.Cars;



public class CarService : ICarService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateCarViewModel> createCarModelValidator;
    private readonly IModelValidator<UpdateCarViewModel> updateCarModelValidator;
    private readonly IHubContext<CarHub> hubContext;
    private readonly IAction action;

    public CarService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper
        ,IModelValidator<CreateCarViewModel> createCarModelValidator
        ,IModelValidator<UpdateCarViewModel> updateCarModelValidator
        ,IHubContext<CarHub> hubContext
        ,IAction action
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createCarModelValidator = createCarModelValidator;
        this.updateCarModelValidator = updateCarModelValidator;
        this.hubContext = hubContext;
        this.action = action;
    }
   public async Task<IEnumerable<CarViewModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var cars = await context.Cars
            .Include(s => s.Seller)
            .Include(s => s.ViewingRequestsCar)
            .ToListAsync();

        var res = mapper.Map<IEnumerable<CarViewModel>>(cars);
        return res;
    }
    public async Task<CarViewModel> GetById(Guid carId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars
                        .Include(s => s.Seller)
                        .Include(s => s.ViewingRequestsCar)
                        .FirstOrDefaultAsync(x => x.Uid == carId);

        var res = mapper.Map<CarViewModel>(car);
        return res;
    }
    public async Task<CarViewModel> Create(CreateCarViewModel model)
    {
        await createCarModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = mapper.Map<Car>(model);

        await context.Cars.AddAsync(car);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData();
            var sellersEmail = context.Sellers.Select(s => s.Email).ToList();
            var sellerFullName = context.Sellers.Where(x => x.Id == car.SellerId).Select(s => s.FullName).FirstOrDefault();
            await action.SendMail(new EmailSendModel()
            {
                Receiver = sellersEmail,
                Subject = "A new car sale announcement has been published",
                Body = $"A new car has been published on the service. Its characteristics:" +
                $"\r\nModel:" + (string.IsNullOrEmpty(car.Model) ? "Not specified" : car.Model) +
                $"\r\nYear:" + (string.IsNullOrEmpty(car.Year.ToString()) ? "Not specified" : car.Year.ToString()) +
                $"\r\nColor:" + (string.IsNullOrEmpty(car.Color) ? "Not specified" : car.Color) +
                $"\r\nFull name of the seller:" + (string.IsNullOrEmpty(sellerFullName) ? "Not specified" : sellerFullName)
            });

        }
        return mapper.Map<CarViewModel>(car);
    }
    public async Task Update(Guid id, UpdateCarViewModel model)
    {
        
        await updateCarModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Where(x => x.Uid == id).FirstOrDefaultAsync();

        car = mapper.Map(model, car);

        context.Cars.Update(car);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData();
        }
    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (car == null)
            throw new ProcessException($"car (ID = {id}) not found.");

        context.Cars.Remove(car);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData();
        }
    }

    public async Task<IEnumerable<CarViewModel>> GetMyCars(string username)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var cars = await context.Cars
            .Include(s => s.Seller)
            .Include(s => s.ViewingRequestsCar)
            .Where(s => s.Seller.Username == username)
            .ToListAsync();

        var res = mapper.Map<IEnumerable<CarViewModel>>(cars);
        return res;
    }

    public async Task SendCommandForUpdateData()
    {
        await hubContext.Clients.All.SendAsync("ReceiveCarUpdate","555");
    }
}

