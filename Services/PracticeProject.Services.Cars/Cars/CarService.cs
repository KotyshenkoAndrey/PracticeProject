using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Common.Exceptions;
using PracticeProject.Context;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars;



public class CarService : ICarService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public CarService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
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
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = mapper.Map<Car>(model);

        await context.Cars.AddAsync(car);

        await context.SaveChangesAsync();

        return mapper.Map<CarViewModel>(car);
    }
    public async Task Update(Guid id, UpdateCarViewModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Where(x => x.Uid == id).FirstOrDefaultAsync();

        car = mapper.Map(model, car);

        context.Cars.Update(car);

        await context.SaveChangesAsync();
    }
    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var car = await context.Cars.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (car == null)
            throw new ProcessException($"car (ID = {id}) not found.");

        context.Cars.Remove(car);

        await context.SaveChangesAsync();
    }
}

