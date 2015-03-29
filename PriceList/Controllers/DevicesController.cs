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

namespace PriceList.Controllers
{
    public class DevicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Devices
        public IQueryable<Device> GetDevices()
        {
            return db.Devices;
        }
       
        [ActionName("ByKeyword"), HttpGet]
        public IEnumerable<Device> ByKeyword(string q="")
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false; 
            var collect = (from device in db.Devices
                           where device.Fullname.Contains(q)
                           select device).AsEnumerable();
            return collect;
        }
        //[ActionName("ByKeyword"), HttpGet]
        //public IDictionary<int, string> ByKeyword(string term)
        //{
        //    var collect = (from device in db.Devices
        //                   let fulname = device.Model.Brand.BrandName + " " + device.Model.ModelName + " " + device.Specification
        //                   where fulname.Contains(term)
        //                   select new { device.ID, fulname }).AsEnumerable();
        //    var collect = (from device in db.Devices
        //                   where device.Fullname.Contains(term)
        //                   select new { device.ID, device.Fullname }).AsEnumerable();
        //    return collect.ToDictionary(x => x.ID, x => x.Fullname);


        //}
        //// GET: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(int id)
        {
            Device device = await db.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevice(int id, Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.ID)
            {
                return BadRequest();
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Devices
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Devices.Add(device);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = device.ID }, device);
        }

        // DELETE: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> DeleteDevice(int id)
        {
            Device device = await db.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            db.Devices.Remove(device);
            await db.SaveChangesAsync();

            return Ok(device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(int id)
        {
            return db.Devices.Count(e => e.ID == id) > 0;
        }
    }
}