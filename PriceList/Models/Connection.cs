using System;
using System.ComponentModel.DataAnnotations;
namespace PriceList.Models
{
    public class Connection
    {
        public string User { get; set; }
        public string ConnectionID { get; set; }
        public DateTime ConnectedDate { get; set; }
    }
}