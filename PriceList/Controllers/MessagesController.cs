using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PriceList.Models;
using Newtonsoft.Json;
using System.Web.Helpers;
using PriceList.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace PriceList.Controllers
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: api/Messages
        public IEnumerable<MessageViewModel> GetMessages()
        {
            var userId=RequestContext.Principal.Identity.GetUserId();

            db.Configuration.ProxyCreationEnabled = false;
            var msgs = db.Messages.Include(m => m.User)
                .Include(m => m.AskedDevice)
                .Include(r => r.Replies).AsEnumerable();
            var msgsEdited = from m in msgs
                             orderby m.PostDate
                             select new MessageViewModel
                             {
                                 ID = m.ID,
                                 MessageText = m.MessageText,
                                 PostDate = m.PostDate,
                                 UserName = m.User.UserName,
                                 UserFullName = m.User.FirstName,
                                 UserCompany = m.User.CompanyName,
                                 DeviceName = m.AskedDevice.Fullname,
                                 //BrandID = m.AskedDevice.Model.BrandID,
                                 ModelID = m.AskedDevice.ModelID,
                                 Replies = (from r in m.Replies
                                            where r.User.Id == userId
                                            orderby r.PostDate
                                            select new ReplyViewModel
                                            {
                                                ID = r.ID,
                                                Amount = r.Amount,
                                                MessageID = m.ID,
                                                UserName = m.User.UserName,
                                                UserFullName = m.User.FirstName,
                                                UserCompany = m.User.CompanyName
                                            }).ToList()
                             };
            return msgsEdited;
        }

        //[ActionName("PreviousMessages")]
        //public async Task<IEnumerable<Message>> GetMessages(int count)
        //{
        //    var previousMessages = await (from m in db.Messages
        //                                  orderby m.PostDate descending
        //                                  select m).Take(count).Include(m => m.User)
        //        .Include(m => m.AskedDevice).ToListAsync();


        //    return previousMessages.AsEnumerable()
        //                           .Reverse();
        //}
        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> GetMessage(Guid id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(Guid id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.ID)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(message.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = message.ID }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(Guid id)
        {
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            await db.SaveChangesAsync();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(Guid id)
        {
            return db.Messages.Count(e => e.ID == id) > 0;
        }
    }
}