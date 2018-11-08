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
    public class SemesterController : ApiController
    {
        public SemesterController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Semester
        public IQueryable<Semester> GetSemesters()
        {
            return db.Semesters;
        }

        // GET: api/Semester/5
        [ResponseType(typeof(Semester))]
        public async Task<IHttpActionResult> GetSemester(long id)
        {
            Semester semester = await db.Semesters.FindAsync(id);
            if (semester == null)
            {
                return NotFound();
            }

            return Ok(semester);
        }

        // PUT: api/Semester/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSemester(long id, Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != semester.SemesterID)
            {
                return BadRequest();
            }

            db.Entry(semester).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemesterExists(id))
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

        // POST: api/Semester
        [ResponseType(typeof(Semester))]
        public async Task<IHttpActionResult> PostSemester(Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Semesters.Add(semester);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = semester.SemesterID }, semester);
        }

        // DELETE: api/Semester/5
        [ResponseType(typeof(Semester))]
        public async Task<IHttpActionResult> DeleteSemester(long id)
        {
            Semester semester = await db.Semesters.FindAsync(id);
            if (semester == null)
            {
                return NotFound();
            }

            db.Semesters.Remove(semester);
            await db.SaveChangesAsync();

            return Ok(semester);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SemesterExists(long id)
        {
            return db.Semesters.Count(e => e.SemesterID == id) > 0;
        }
    }
}