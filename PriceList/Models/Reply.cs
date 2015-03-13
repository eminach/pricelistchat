using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PriceList.Models
{
    public class Reply
    {
        [Key]
        public Guid ID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime PostDate { get; set; }
        //[ForeignKey("MessageID")]
        public virtual Message Message { get; set; }
    }
}