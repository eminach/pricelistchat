using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PriceList.Models
{
    [JsonObject(IsReference = true)] 
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
    public class DeviceList
    {
        public int ID { get; set; }
        public string FullName { get; set; }
    }
}