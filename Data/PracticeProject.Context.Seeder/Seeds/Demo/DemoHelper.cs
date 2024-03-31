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
            Seller = new User()
            {
                Uid = Guid.NewGuid(),
                Username = "Andrey",  
                Email = "andrey@test.ru",
                FullName = "andrey andrey"
            },            
        },
        new Car()
        {
            Uid = Guid.NewGuid(),
            Model = "Volkswagen Passat",
            Price = 1000000,
            DatePosted = DateTime.Now,
            Seller = new User()
            {
                Uid = Guid.NewGuid(),
                Username = "Victor",
                Email = "victor@test.ru",
                FullName = "victor victor"
            },
        },
    };
}