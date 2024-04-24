using PracticeProject.Services.Cars.Models;

namespace PracticeProject.Services.Cars;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAll();
    Task<CarViewModel> GetById(Guid carId);
    Task<CarViewModel> Create(CreateCarViewModel model);
    Task Update(Guid id, UpdateCarViewModel model);
    Task Delete(Guid id);
    Task<IEnumerable<CarViewModel>> GetMyCars(string username);

}

