using System.Net.Http.Json;
using PracticeProject.Web.Cars.Models;

namespace PracticeProject.Web.Pages.Car.Services;

public class CarService(HttpClient httpClient) : ICarService
{
    public async Task<IEnumerable<CarViewModel>> GetAllCars()
    {
        var response = await httpClient.GetAsync("/getallcars");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<CarViewModel>>() ?? new List<CarViewModel>();
    }

    public async Task<CarViewModel> GetCarById(Guid carId)
    {
        var response = await httpClient.GetAsync($"/getcarbyid/{carId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<CarViewModel>() ?? new();
    }

    public async Task AddCar(CreateCarViewModel model)
    {

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("/addcar/", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditCar(Guid carId, UpdateCarViewModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"/editcar/{carId}", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteCar(Guid carId)
    {
        var response = await httpClient.DeleteAsync($"/deletecar/{carId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task<IEnumerable<SellerViewModel>> GetSellers()
    {
        var response = await httpClient.GetAsync("/getsellers");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<SellerViewModel>>() ?? new List<SellerViewModel>();
    }
}