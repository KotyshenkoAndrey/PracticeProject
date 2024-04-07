using AutoMapper;
using PracticeProject.Context.Entities;

namespace PracticeProject.Services.Cars;

public class UpdateCarViewModel
{
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int? Year { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
    public DateTime DatePosted { get; set; }
}
public class UpdateCarViewModelProfile : Profile
{
    public UpdateCarViewModelProfile()
    {
        CreateMap<UpdateCarViewModel, Car>();
    }
}