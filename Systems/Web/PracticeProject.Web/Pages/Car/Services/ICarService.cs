using PracticeProject.Web.Cars.Models;

namespace PracticeProject.Web.Pages.Car.Services;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAllCars();
    Task<CarViewModel> GetCarById(Guid carId);
    Task AddCar(CreateCarViewModel model);
    Task EditCar(Guid carId, UpdateCarViewModel model);
    Task DeleteCar(Guid carId);
    Task<IEnumerable<SellerViewModel>> GetSellers();
    Task<IEnumerable<CarViewModel>> GetMyCars();
}