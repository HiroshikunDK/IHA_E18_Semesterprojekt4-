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
    public class RatingController : ApiController
    {
        public RatingController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Rating
        public IQueryable<Rating> GetRatings(long QuizID = -1)
        {
            if (QuizID != -1)
            {
                return db.Ratings.Where(r => r.QuizID == QuizID);
            }
            return db.Ratings;
        }

        // GET: api/Rating/5
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> GetRating(long id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Rating/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRating(long id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RatingID)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingID }, rating);
        }

        // DELETE: api/Rating/5
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> DeleteRating(long id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            await db.SaveChangesAsync();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(long id)
        {
            return db.Ratings.Count(e => e.RatingID == id) > 0;
        }
    }
}