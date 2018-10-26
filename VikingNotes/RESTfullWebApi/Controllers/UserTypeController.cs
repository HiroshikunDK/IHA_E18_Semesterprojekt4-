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
    public class UserTypeController : ApiController
    {
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/UserType
        public IQueryable<UserType> GetUserTypes()
        {
            return db.UserTypes;
        }

        // GET: api/UserType/5
        [ResponseType(typeof(UserType))]
        public IHttpActionResult GetUserType(long id)
        {
            UserType userType = db.UserTypes.Find(id);
            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        // PUT: api/UserType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserType(long id, UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userType.UserTypeID)
            {
                return BadRequest();
            }

            db.Entry(userType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserType
        [ResponseType(typeof(UserType))]
        public IHttpActionResult PostUserType(UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTypes.Add(userType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userType.UserTypeID }, userType);
        }

        // DELETE: api/UserType/5
        [ResponseType(typeof(UserType))]
        public IHttpActionResult DeleteUserType(long id)
        {
            UserType userType = db.UserTypes.Find(id);
            if (userType == null)
            {
                return NotFound();
            }

            db.UserTypes.Remove(userType);
            db.SaveChanges();

            return Ok(userType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTypeExists(long id)
        {
            return db.UserTypes.Count(e => e.UserTypeID == id) > 0;
        }
    }
}