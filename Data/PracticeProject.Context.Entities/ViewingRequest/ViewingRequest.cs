using System.ComponentModel.DataAnnotations;

namespace PracticeProject.Context.Entities
{
    public class ViewingRequest : BaseEntity
    {
        [Required]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required]
        public int SenderId { get; set; }
        public virtual Seller Sender { get; set; }

        public StatusConfirm StateConfirmed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RequestDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastModifedDate { get; set; }
    }
}
