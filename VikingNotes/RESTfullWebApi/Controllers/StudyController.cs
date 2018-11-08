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
    public class StudyController : ApiController
    {
        public StudyController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Study
        public IQueryable<Study> GetStudies()
        {
            return db.Studies;
        }

        // GET: api/Study/5
        [ResponseType(typeof(Study))]
        public async Task<IHttpActionResult> GetStudy(long id)
        {
            Study study = await db.Studies.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }

            return Ok(study);
        }

        // PUT: api/Study/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStudy(long id, Study study)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != study.StudyID)
            {
                return BadRequest();
            }

            db.Entry(study).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyExists(id))
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

        // POST: api/Study
        [ResponseType(typeof(Study))]
        public async Task<IHttpActionResult> PostStudy(Study study)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Studies.Add(study);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = study.StudyID }, study);
        }

        // DELETE: api/Study/5
        [ResponseType(typeof(Study))]
        public async Task<IHttpActionResult> DeleteStudy(long id)
        {
            Study study = await db.Studies.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }

            db.Studies.Remove(study);
            await db.SaveChangesAsync();

            return Ok(study);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudyExists(long id)
        {
            return db.Studies.Count(e => e.StudyID == id) > 0;
        }
    }
}