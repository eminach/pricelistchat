using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PriceList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PriceList
{

    public class ChatHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public override System.Threading.Tasks.Task OnConnected()
        {
            db.Connections.Add(new Connection() { ConnectionID = Context.ConnectionId, ConnectedDate = DateTime.Now, User = Context.User.Identity.Name });
            db.SaveChangesAsync();
            return base.OnConnected();
        }
        

        public void Send(string message, int device)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var user = db.Users.Include("Type").AsEnumerable();
            var curUser = user.First(x => x.Id.Equals(Context.User.Identity.GetUserId()));
            Message msg = new Message()
            {
                ID = Guid.NewGuid(),
                PostDate = DateTime.Now,
                MessageText = message,
                User = curUser,//GetCurrentUser(),
                AskedDevice=GetDeviceByID(device)
            };

            SaveMessage(msg);
         
            var jMessage = JsonConvert.SerializeObject(msg);

            Clients.All.broadcastMessage(jMessage);
        }

        private Device GetDeviceByID(int device)
        {
            return db.Devices.First(d => d.ID == device);
        }
        public void SaveMessage(Message msg)
        {
            using (db)
            {
                db.Messages.Add(msg);
                db.SaveChanges();
            }
        }
        //public async Task GetPreviousMessages()
        //{
        //    var previousMessages = await (from m in db.Messages
        //                                  orderby m.PostDate descending
        //                                  select m).Take(20).ToListAsync();


        //    await Clients.All.previousMessages(previousMessages.AsEnumerable().Reverse());
        //}
       
        private ApplicationUser GetCurrentUser()
        {
            var store = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(store);
            return userManager.FindById(Context.User.Identity.GetUserId());
        }
    }
}