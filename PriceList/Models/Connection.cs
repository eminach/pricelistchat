using System;
using System.ComponentModel.DataAnnotations;
namespace PriceList.Models
{
    public class Connection
    {
        public string User { get; set; }
        public string ConnectionID { get; set; }
        public DateTime ConnectedDate { get; set; }
        public ConnectionStatus Status { get; set; }
    }
    public enum ConnectionStatus
    {        
        Offline=0,
        Online=1, 
        Disconnected=2
    }
}