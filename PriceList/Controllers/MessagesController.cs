using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PriceList.Models;
using Newtonsoft.Json;

namespace PriceList.Controllers
{
    public class MessagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
                                                               
        // GET: api/Messages
        public IEnumerable<Message> GetMessages()
        {
            //return db.Messages.ToList();
            //db.Configuration.LazyLoadingEnabled = false;
           db.Configuration.ProxyCreationEnabled = false; 
            var msgs = db.Messages.Include(m => m.User)
                .Include(m => m.AskedDevice)
                .Include(r=>r.Replies).AsEnumerable();
            return msgs;
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