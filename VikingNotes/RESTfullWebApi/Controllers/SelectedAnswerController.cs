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
    public class SelectedAnswerController : ApiController
    {
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/SelectedAnswer
        public IQueryable<SelectedAnswer> GetSelectedAnswers()
        {
            return db.SelectedAnswers;
        }

        // GET: api/SelectedAnswer/5
        [ResponseType(typeof(SelectedAnswer))]
        public async Task<IHttpActionResult> GetSelectedAnswer(long id)
        {
            SelectedAnswer selectedAnswer = await db.SelectedAnswers.FindAsync(id);
            if (selectedAnswer == null)
            {
                return NotFound();
            }

            return Ok(selectedAnswer);
        }

        // PUT: api/SelectedAnswer/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelectedAnswer(long id, SelectedAnswer selectedAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectedAnswer.SelectedAnswerID)
            {
                return BadRequest();
            }

            db.Entry(selectedAnswer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectedAnswerExists(id))
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

        // POST: api/SelectedAnswer
        [ResponseType(typeof(SelectedAnswer))]
        public async Task<IHttpActionResult> PostSelectedAnswer(SelectedAnswer selectedAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SelectedAnswers.Add(selectedAnswer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selectedAnswer.SelectedAnswerID }, selectedAnswer);
        }

        // DELETE: api/SelectedAnswer/5
        [ResponseType(typeof(SelectedAnswer))]
        public async Task<IHttpActionResult> DeleteSelectedAnswer(long id)
        {
            SelectedAnswer selectedAnswer = await db.SelectedAnswers.FindAsync(id);
            if (selectedAnswer == null)
            {
                return NotFound();
            }

            db.SelectedAnswers.Remove(selectedAnswer);
            await db.SaveChangesAsync();

            return Ok(selectedAnswer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectedAnswerExists(long id)
        {
            return db.SelectedAnswers.Count(e => e.SelectedAnswerID == id) > 0;
        }
    }
}