using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RESTfullWebApi.Models;

namespace RESTfullWebApi.Controllers
{
    public class UserrsController : ApiController
    {
        public UserrsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Userrs
        public IQueryable<Userr> GetUserrs()
        {
            return db.Userrs;
        }

        // GET: api/Userrs/5
        [ResponseType(typeof(Userr))]
        public IHttpActionResult GetUserr(long id)
        {
            Userr userr = db.Userrs.Find(id);
            if (userr == null)
            {
                return NotFound();
            }

            return Ok(userr);
        }

        // PUT: api/Userrs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserr(long id, Userr userr)
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
                db.SaveChanges();
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

        // POST: api/Userrs
        [ResponseType(typeof(Userr))]
        public IHttpActionResult PostUserr(Userr userr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Userrs.Add(userr);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userr.UserID }, userr);
        }

        // DELETE: api/Userrs/5
        [ResponseType(typeof(Userr))]
        public IHttpActionResult DeleteUserr(long id)
        {
            Userr userr = db.Userrs.Find(id);
            if (userr == null)
            {
                return NotFound();
            }

            db.Userrs.Remove(userr);
            db.SaveChanges();

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