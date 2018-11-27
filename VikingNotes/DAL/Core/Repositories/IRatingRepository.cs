using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfullWebApi.Models;

namespace DAL.Core.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Task<List<Rating>> GetRatingByQuizID(long quizID);
    }
}
