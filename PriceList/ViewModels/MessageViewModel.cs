using PriceList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriceList.ViewModels
{
    public class MessageViewModel
    {
        public Guid ID { get; set; }

        public DateTime PostDate { get; set; }

        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string UserCompany { get; set; }

        public string MessageText { get; set; }

        public string DeviceName { get; set; }

        public int DeviceID { get; set; }

        public int? ModelID { get; set; }

        public int? BrandID { get; set; }

        public virtual ICollection<ReplyViewModel> Replies { get; set; }

    }
}