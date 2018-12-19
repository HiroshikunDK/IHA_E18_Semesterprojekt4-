using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RESTfullWebApi.Models;

namespace RESTfullWebApi.Controllers
{
    public class UserrController : ApiController
    {
        public UserrController()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            db.Database.Log = sql => Debug.Write(sql);
            
        }
        private VikingNoteDBEntities db = new VikingNoteDBEntities();

        // GET: api/Userr
        public async Task<IQueryable<UserrDTO>> GetUserrs(string username = null, string password = null)
        {
            if (username != null && password != null)
            {
                List<UserrDTO> list = new List<UserrDTO>();
                Userr userTryinToLogIn = db.Userrs.FirstOrDefault(u => u.UserName == username);
                if (userTryinToLogIn == null)
                {
                    return list.AsQueryable();
                }
                string[] passwordHash = GetHash.SHA256(password, userTryinToLogIn.Salt);
                string passwordToCheck = passwordHash[0];
                Userr loggedInUserr = db.Userrs.FirstOrDefault(u => u.UserName == username && u.Password.Trim() == passwordToCheck);
                if (loggedInUserr == null)
                {
                    return list.AsQueryable();
                }
                // Generer en ny authtoken.
                loggedInUserr.AuthToken = GetHash.SHA256(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))[0];
                db.Entry(loggedInUserr).State = EntityState.Modified;
                await db.SaveChangesAsync();

                UserrDTO transferUserObject = new UserrDTO()
                {
                    UserID = loggedInUserr.UserID,
                    EmailAdress = loggedInUserr.EmailAdress,
                    StudentNumber = loggedInUserr.StudentNumber,
                    UserName = loggedInUserr.UserName,
                    StudyID =  loggedInUserr.StudyID,
                    UserTypeID = loggedInUserr.UserTypeID,
                    AuthToken = loggedInUserr.AuthToken

                };
                list.Add(transferUserObject);
                return list.AsQueryable();
            }

            var users = from u in db.Userrs
                select new UserrDTO()
                {
                    UserID = u.UserID,
                    EmailAdress = u.EmailAdress,
                    StudentNumber = u.StudentNumber,
                    UserName = u.UserName,
                    StudyID = u.StudyID,
                    UserTypeID = u.UserTypeID
                };
            return users;
        }

        // GET: api/Userr/5
        [ResponseType(typeof(UserrDTO))]
        public async Task<IHttpActionResult> GetUserr(long id)
        {
            
            Userr userr = await db.Userrs.FindAsync(id);
            if (userr == null)
            {
                return NotFound();
            }
            UserrDTO transferUserObject = new UserrDTO()
            {
                UserID = userr.UserID,
                EmailAdress = userr.EmailAdress,
                StudentNumber = userr.StudentNumber,
                UserName = userr.UserName,
                StudyID = userr.StudyID,
                UserTypeID = userr.UserTypeID

            };
            return Ok(transferUserObject);
        }

        // PUT: api/Userr/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserr(long id,[FromBody]Userr userr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userr.UserID)
            {
                return BadRequest();
            }

            string[] passwordParameters = GetHash.SHA256(userr.Password);
            userr.Password = passwordParameters[0];
            userr.Salt = passwordParameters[1];
            db.Entry(userr).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

        // POST: api/Userr
        [ResponseType(typeof(UserrDTO))]
        
        public async Task<IHttpActionResult> PostUserr([FromBody]Userr userr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string[] passwordParameters = GetHash.SHA256(userr.Password);
            userr.Password = passwordParameters[0];
            userr.Salt = passwordParameters[1];
            userr.AuthToken = GetHash.SHA256(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))[0];
            db.Userrs.Add(userr);
            await db.SaveChangesAsync();

            UserrDTO transferUserObject = new UserrDTO()
            {
                UserID = userr.UserID,
                EmailAdress = userr.EmailAdress,
                StudentNumber = userr.StudentNumber,
                UserName = userr.UserName,
                StudyID = userr.StudyID,
                UserTypeID = userr.UserTypeID,
                AuthToken = userr.AuthToken

            };
            return Ok(transferUserObject);
        }

        // DELETE: api/Userr/5
        [ResponseType(typeof(Userr))]
        public async Task<IHttpActionResult> DeleteUserr(long id)
        {
            Userr userr = await db.Userrs.FindAsync(id);
            if (userr == null)
            {
                return NotFound();
            }

            db.Userrs.Remove(userr);
            await db.SaveChangesAsync();

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