using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriceList.ViewModels
{
    public class ReplyViewModel
    { 
        public Guid ID { get; set; }
     
        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string UserCompany { get; set; }
        
        public decimal Amount { get; set; }
        
        public DateTime PostDate { get; set; }
        
        public Guid MessageID { get; set; }
    }
}