using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeProject.Context.Entities
{
    public class User : BaseEntity
    {
        
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        //        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        public string? FullName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public virtual ICollection<Car> CarsUser { get; set; }

        public virtual ICollection<ViewingRequest>? ViewingRequestsUser { get; set; }
    }
}
