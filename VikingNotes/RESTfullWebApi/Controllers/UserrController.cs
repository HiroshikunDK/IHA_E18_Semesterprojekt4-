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
using RESTfullWebApi.Models;

namespace RESTfullWebApi.Controllers
{
    public class UserrController : ApiController
    {
        public UserrController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Userr
        public IQueryable<Userr> GetUserrs()
        {
            return db.Userrs;
        }

        // GET: api/Userr/5
        [ResponseType(typeof(Userr))]
        public async Task<IHttpActionResult> GetUserr(long id)
        {
            Userr userr = await db.Userrs.FindAsync(id);
            if (userr == null)
            {
                return NotFound();
            }

            return Ok(userr);
        }

        // PUT: api/Userr/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserr(long id, Userr userr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userr.UserID)
            {
                return BadRequest();
            }

            db.Entry(userr).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserrExists(id))
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

        // POST: api/Userr
        [ResponseType(typeof(Userr))]
        public async Task<IHttpActionResult> PostUserr(Userr userr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Userrs.Add(userr);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userr.UserID }, userr);
        }

        // DELETE: api/Userr/5
        [ResponseType(typeof(Userr))]
        public async Task<IHttpActionResult> DeleteUserr(long id)
        {
            Userr userr = await db.Userrs.FindAsync(id);
            if (userr == null)
            {
                return NotFound();
            }

            db.Userrs.Remove(userr);
            await db.SaveChangesAsync();

            return Ok(userr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserrExists(long id)
        {
            return db.Userrs.Count(e => e.UserID == id) > 0;
        }
    }
}