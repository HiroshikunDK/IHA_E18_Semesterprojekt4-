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
    public class CatagoryController : ApiController
    {
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Catagory
        public IQueryable<Catagory> GetCatagories()
        {
            return db.Catagories;
        }

        // GET: api/Catagory/5
        [ResponseType(typeof(Catagory))]
        public IHttpActionResult GetCatagory(long id)
        {
            Catagory catagory = db.Catagories.Find(id);
            if (catagory == null)
            {
                return NotFound();
            }

            return Ok(catagory);
        }

        // PUT: api/Catagory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCatagory(long id, Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catagory.CatagoryID)
            {
                return BadRequest();
            }

            db.Entry(catagory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatagoryExists(id))
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

        // POST: api/Catagory
        [ResponseType(typeof(Catagory))]
        public IHttpActionResult PostCatagory(Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Catagories.Add(catagory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = catagory.CatagoryID }, catagory);
        }

        // DELETE: api/Catagory/5
        [ResponseType(typeof(Catagory))]
        public IHttpActionResult DeleteCatagory(long id)
        {
            Catagory catagory = db.Catagories.Find(id);
            if (catagory == null)
            {
                return NotFound();
            }

            db.Catagories.Remove(catagory);
            db.SaveChanges();

            return Ok(catagory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CatagoryExists(long id)
        {
            return db.Catagories.Count(e => e.CatagoryID == id) > 0;
        }
    }
}