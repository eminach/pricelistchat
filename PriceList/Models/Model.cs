using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceList.Models
{
    public class Model
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string ModelName { get; set; }
        
        [StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? BrandID { get; set; }

        [Required]
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

    }
}