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
            var curUser = GetCurrentUser();
            if (curUser != null)
            {
                db.Connections.Add(new Connection() { ConnectionID = Context.ConnectionId, ConnectedDate = DateTime.Now, User = Context.User.Identity.Name, Status = ConnectionStatus.Online });
                curUser.Status = ConnectionStatus.Online;
                db.SaveChangesAsync();
            }
            ShowOnlineUsers();
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var connectedUsers = db.Connections.Where(c => c.User == Context.User.Identity.Name);
            foreach (var item in connectedUsers)
            {
                item.Status = ConnectionStatus.Disconnected;
            }
            var curUser = GetCurrentUser();
            if (curUser != null)
                curUser.Status = ConnectionStatus.Disconnected;
            db.SaveChangesAsync();
            return base.OnDisconnected(stopCalled);
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
                AskedDevice = GetDeviceByID(device)
            };

            SaveMessage(msg);

           // var jMessage = JsonConvert.SerializeObject(msg);

            Clients.All.broadcastMessage(msg);
        }
        public void Reply(object id, string amount)
        {
            Guid mID = Guid.Parse(id.ToString());
            var message = db.Messages.Find(mID);
            message.Replies.Add(new Reply()
            {
                ID=Guid.NewGuid(),
                Amount=decimal.Parse(amount),
                PostDate = DateTime.Now,
                User = GetCurrentUser()
            });
            db.SaveChangesAsync();
            Clients.All.broadcastReply(id, amount);
        }
        public async Task ShowOnlineUsers()
        {
            var ActiveUsers = await db.Users.Where(c => c.Status == ConnectionStatus.Online).Select(u => new { u.CompanyName, u.FirstName }).ToListAsync();
            //  Dictionary<string, string> usersList = new Dictionary<string, string>();
            // var jUsers = JsonConvert.SerializeObject(ActiveUsers);

            Clients.All.activeUsersList(ActiveUsers);
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