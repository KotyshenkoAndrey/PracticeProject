using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Context.Entities
{
    public class ViewingRequest : BaseEntity
    {
        [Required]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required]
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }

        public bool IsConfirmed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }
    }
}
