using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PriceList.Models
{
    public class Brand
    {

        [Key]
        public int ID { get; set; }

       // [Required, StringLength(100), Display(Name = "Name")]
        public string BrandName { get; set; }

      //  [Required, StringLength(10000), Display(Name = "Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
         
       // [Display(Name="Logo")]
        public string LogoPath { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}