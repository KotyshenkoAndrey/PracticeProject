using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Services.Actions;
using PracticeProject.Common.Exceptions;
using PracticeProject.Common.Validator;
using PracticeProject.Context;
using PracticeProject.Context.Entities;
using PracticeProject.Services.Cars.Models;
using Microsoft.AspNetCore.SignalR;
using PracticeProject.Services.AppHubs;
using PracticeProject.Services.Cache;

namespace PracticeProject.Services.Cars;



public class CarService : ICarService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<CreateCarViewModel> createCarModelValidator;
    private readonly IModelValidator<UpdateCarViewModel> updateCarModelValidator;
    private readonly IHubContext<AppHub> hubContext;
    private readonly ICacheService cacheService;
    private readonly IAction action;
    private const string cacheKeyForAllCar = nameof(cacheKeyForAllCar);
    private const string cacheKeyForUser = nameof(cacheKeyForUser);

    public CarService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper
        ,IModelValidator<CreateCarViewModel> createCarModelValidator
        ,IModelValidator<UpdateCarViewModel> updateCarModelValidator
        ,IHubContext<AppHub> hubContext
        ,ICacheService cacheService
        ,IAction action
        )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.createCarModelValidator = createCarModelValidator;
        this.updateCarModelValidator = updateCarModelValidator;
        this.hubContext = hubContext;
        this.cacheService = cacheService;
        this.action = action;
    }
   public async Task<IEnumerable<CarViewModel>> GetAll()
    {
        var carsFromCache = await GetCarFromCache(cacheKeyForAllCar);

        if (carsFromCache != null)
            return carsFromCache;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var cars = await context.Cars
            .Include(s => s.Seller)
            .Include(s => s.ViewingRequestsCar)
            .ToListAsync();

        var carModels = mapper.Map<IEnumerable<CarViewModel>>(cars);

        try
        {
            await cacheService.Put(cacheKeyForAllCar, carModels, TimeSpan.FromMinutes(5));
        }
        catch (Exception ex) { /* TODO записать в логи, что не удалось записать в кэш*/}

        return carModels;
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
            var seller = context.Sellers.Where(x => x.Id == car.SellerId).FirstOrDefault();
            var sellersEmail = context.Sellers.Select(s => s.Email).ToList();

            await SendCommandForUpdateData(seller.Username);
                        
            await action.SendMail(new EmailSendModel()
            {
                Receiver = sellersEmail,
                Subject = "A new car sale announcement has been published",
                Body = $"A new car has been published on the service. Its characteristics:" +
                $"\r\nModel:" + (string.IsNullOrEmpty(car.Model) ? "Not specified" : car.Model) +
                $"\r\nYear:" + (string.IsNullOrEmpty(car.Year.ToString()) ? "Not specified" : car.Year.ToString()) +
                $"\r\nColor:" + (string.IsNullOrEmpty(car.Color) ? "Not specified" : car.Color) +
                $"\r\nFull name of the seller:" + (string.IsNullOrEmpty(seller.FullName) ? "Not specified" : seller.FullName)
            });            
        }        

        return mapper.Map<CarViewModel>(car);
    }

    async private void ClearCache(List<string> listCacheKey)
    {
        try
        {
            foreach (var key in listCacheKey)
            {
                await cacheService.Delete(key);
            }
        }
        catch (Exception ex)
        {

        }
    }

    async private Task<IEnumerable<CarViewModel>?> GetCarFromCache(string listCacheKey)
    {
        try
        {
            var cacheData = await cacheService.Get<IEnumerable<CarViewModel>>(listCacheKey);
            if (cacheData != null)
            {
                return cacheData;
            }
        }
        catch (Exception ex)
        {
            //TODO записать в логи ошибку
        }
        return null;
    }


    public async Task Update(Guid id, UpdateCarViewModel model)
    {
        
        await updateCarModelValidator.CheckAsync(model);

        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Include(s=> s.Seller).Where(x => x.Uid == id).FirstOrDefaultAsync();

        car = mapper.Map(model, car);

        context.Cars.Update(car);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData(car.Seller.Username);
        }
    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Include(s => s.Seller).Where(x => x.Uid == id).FirstOrDefaultAsync();
        if (car == null)
            throw new ProcessException($"car (ID = {id}) not found.");

        context.Cars.Remove(car);

        int savedChanges = await context.SaveChangesAsync();
        if (savedChanges > 0)
        {
            await SendCommandForUpdateData(car.Seller.Username);
        }
    }

    public async Task<IEnumerable<CarViewModel>> GetMyCars(string username)
    {
        var carsFromCache = await GetCarFromCache($"{cacheKeyForUser}/{username}");

        if (carsFromCache != null)
            return carsFromCache;

        using var context = await dbContextFactory.CreateDbContextAsync();

        var cars = await context.Cars
            .Include(s => s.Seller)
            .Include(s => s.ViewingRequestsCar)
            .Where(s => s.Seller.Username == username)
            .ToListAsync();

        var carModels = mapper.Map<IEnumerable<CarViewModel>>(cars);

        try
        {
            await cacheService.Put($"{cacheKeyForUser}/{username}", carModels, TimeSpan.FromMinutes(5));
        }
        catch (Exception ex) { /*TODO Записать в логи информацию об ошибке*/}

        return carModels;
        
    }

    public async Task SendCommandForUpdateData(string username = "")
    {
        ClearCache([cacheKeyForAllCar, $"{cacheKeyForUser}/{username}"]);
        await hubContext.Clients.All.SendAsync("ReceiveCarUpdate","update");
        await hubContext.Clients.All.SendAsync("ReceiveIncomeRequestUpdate", "update");
    }
}

