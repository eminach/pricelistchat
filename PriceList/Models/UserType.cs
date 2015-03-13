using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PriceList.Models
{
    public class UserType
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
       // public virtual ICollection<ApplicationUser> Users { get; set; }
        //EF de bele qayda yoxdu, XPO deyil ee :) deqiq? axi one-to-one di hec XPO dada yoxdu
    }
}