using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Bo
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Ssn { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100)]
        [Display(Name = "Address Line")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100)]
        [Display(Name = "Address Line")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Please enter your zip code")]
        [Display(Name = "Zip code")]
        [StringLength(10, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        [StringLength(50)]
        public string Country { get; set; }

        [ScaffoldColumn(false)]
        public float OrderTotal { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderPlaced { get; set; }

    }
}
