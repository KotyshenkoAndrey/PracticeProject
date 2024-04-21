
namespace PracticeProject.Web.Cars.Models;

    public class CarViewModel
    {
        public Guid CarId { get; set; }
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
