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
    public class QuizUserStatisticController : ApiController
    {
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/QuizUserStatistic
        public IQueryable<QuizUserStatistic> GetQuizUserStatistics()
        {
            return db.QuizUserStatistics;
        }

        // GET: api/QuizUserStatistic/5
        [ResponseType(typeof(QuizUserStatistic))]
        public async Task<IHttpActionResult> GetQuizUserStatistic(long id)
        {
            QuizUserStatistic quizUserStatistic = await db.QuizUserStatistics.FindAsync(id);
            if (quizUserStatistic == null)
            {
                return NotFound();
            }

            return Ok(quizUserStatistic);
        }

        // PUT: api/QuizUserStatistic/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuizUserStatistic(long id, QuizUserStatistic quizUserStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quizUserStatistic.QuizUserStatisticID)
            {
                return BadRequest();
            }

            db.Entry(quizUserStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizUserStatisticExists(id))
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

        // POST: api/QuizUserStatistic
        [ResponseType(typeof(QuizUserStatistic))]
        public async Task<IHttpActionResult> PostQuizUserStatistic(QuizUserStatistic quizUserStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.QuizUserStatistics.Add(quizUserStatistic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = quizUserStatistic.QuizUserStatisticID }, quizUserStatistic);
        }

        // DELETE: api/QuizUserStatistic/5
        [ResponseType(typeof(QuizUserStatistic))]
        public async Task<IHttpActionResult> DeleteQuizUserStatistic(long id)
        {
            QuizUserStatistic quizUserStatistic = await db.QuizUserStatistics.FindAsync(id);
            if (quizUserStatistic == null)
            {
                return NotFound();
            }

            db.QuizUserStatistics.Remove(quizUserStatistic);
            await db.SaveChangesAsync();

            return Ok(quizUserStatistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizUserStatisticExists(long id)
        {
            return db.QuizUserStatistics.Count(e => e.QuizUserStatisticID == id) > 0;
        }
    }
}