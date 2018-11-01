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
    public class QuizsController : ApiController
    {
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Quizs
        public IQueryable<Quiz> GetQuizs()
        {
            return db.Quizs;
        }

        // GET: api/Quizs/5
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult GetQuiz(long id)
        {
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/Quizs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuiz(long id, Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.QuizID)
            {
                return BadRequest();
            }

            db.Entry(quiz).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizs
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quizs.Add(quiz);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = quiz.QuizID }, quiz);
        }

        // DELETE: api/Quizs/5
        [ResponseType(typeof(Quiz))]
        public IHttpActionResult DeleteQuiz(long id)
        {
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }

            db.Quizs.Remove(quiz);
            db.SaveChanges();

            return Ok(quiz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizExists(long id)
        {
            return db.Quizs.Count(e => e.QuizID == id) > 0;
        }
    }
}