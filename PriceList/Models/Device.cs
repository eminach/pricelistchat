using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PriceList.Models
{
    public class Device
    {
        [Key]
        public int ID { get; set; }
        public int? ModelID { get; set; }
        [Required]
        public virtual Model Model { get; set; }
        public string Specification { get; set; }

        public string Fullname { get; set; }
    }
    
}