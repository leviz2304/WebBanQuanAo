    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace WebBanHangLapTop.Models
    {
        public class Order
        {
            public int Id { get; set; }
            public string UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
            [StringLength(5000)]
            public string ShippingAddress { get; set; }
            [StringLength(5000)]

            public string Notes { get; set; }
            public string PhoneNumber { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        [ForeignKey("UserId")]
            [ValidateNever]
            public ApplicationUser ApplicationUser { get; set; }
            public List<OrderDetail> OrderDetails { get; set; }
        }
    }
