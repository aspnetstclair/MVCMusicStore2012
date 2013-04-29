using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Display(Name="First Name", Order=1)]
        [Required(ErrorMessage="First name is required")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Last name is required")]
        [StringLength(160)]
        [Display(Name="Last Name", Order=2)]
        public string LastName { get; set; }

        [Required(ErrorMessage="Address is required")]
        [StringLength(70)]
        public string Address { get; set; }

       // [DataType(DataType.Password)]
        [StringLength(40)]
        [Required(ErrorMessage="City is required")]
        public string City { get; set; }

        [Required(ErrorMessage="Province is required")]
        [StringLength(40)]
        public string Province { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

       // [HiddenInput]
        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }

      //  [System.Web.Mvc.Compare("Email")]
        [Required(ErrorMessage="Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Required(ErrorMessage="Email address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", 
            ErrorMessage="Email doesn't look valid")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Range(35,44)]
        //[Range(typeof(decimal), "0.00","49.99")]
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}