using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PriceList.Models
{
    public class CompanyPriceList
    {
        [Key]
        public int ID { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? DeviceID { get; set; }
        [Required]
        public virtual Device Device { get; set; }

        public decimal Amount { get; set; }
    }
}