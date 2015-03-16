using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PriceList.Models
{
    [DataContract(IsReference = true)] 
    public class Reply
    {
        [Key]
        public Guid ID { get; set; }
        [DataMember]
        public virtual ApplicationUser User { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public DateTime PostDate { get; set; }
        [DataMember]
        public virtual Message Message { get; set; }
    }
}