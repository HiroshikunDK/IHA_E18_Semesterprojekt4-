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
    public class FacultyController : ApiController
    {
        public FacultyController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Faculty
        public IQueryable<Faculty> GetFaculties()
        {
            return db.Faculties;
        }

        // GET: api/Faculty/5
        [ResponseType(typeof(Faculty))]
        public async Task<IHttpActionResult> GetFaculty(long id)
        {
            Faculty faculty = await db.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            return Ok(faculty);
        }

        // PUT: api/Faculty/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFaculty(long id, Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faculty.FacultyID)
            {
                return BadRequest();
            }

            db.Entry(faculty).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(id))
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

        // POST: api/Faculty
        [ResponseType(typeof(Faculty))]
        public async Task<IHttpActionResult> PostFaculty(Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faculties.Add(faculty);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = faculty.FacultyID }, faculty);
        }

        // DELETE: api/Faculty/5
        [ResponseType(typeof(Faculty))]
        public async Task<IHttpActionResult> DeleteFaculty(long id)
        {
            Faculty faculty = await db.Faculties.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }

            db.Faculties.Remove(faculty);
            await db.SaveChangesAsync();

            return Ok(faculty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacultyExists(long id)
        {
            return db.Faculties.Count(e => e.FacultyID == id) > 0;
        }
    }
}