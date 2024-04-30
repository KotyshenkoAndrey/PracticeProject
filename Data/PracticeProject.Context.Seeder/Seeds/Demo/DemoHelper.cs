namespace PracticeProject.Context.Seeder;

using PracticeProject.Context.Entities;

public class DemoHelper
{
    public IEnumerable<Car> GetCars = new List<Car>
    {
        new Car()
        {
            Uid = Guid.NewGuid(),
            Model = "Volkswagen Polo",
            Price = 1000000,
            DatePosted = DateTime.Now,
            Seller = new Seller()
            {
                Uid = Guid.NewGuid(),
                Username = "admin",  
                Email = "admin@test.ru",
                FullName = "Administrator"
            },            
        },
    };
}