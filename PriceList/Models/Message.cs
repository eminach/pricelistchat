using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PriceList.Models
{
    //[JsonObject(IsReference = true)] 
    public class Message
    {
        public Message()
        {
            Replies = new List<Reply>();
        }

        [Key]
        public Guid ID { get; set; }

        public DateTime PostDate { get; set; }
        
        public virtual ApplicationUser User { get; set; }

        public string MessageText { get; set; }
        
        public virtual Device AskedDevice { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}