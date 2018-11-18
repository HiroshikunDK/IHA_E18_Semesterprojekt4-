using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Core.Repositories;
using Newtonsoft.Json;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public async Task<List<Rating>> GetRatingsByQuizID(long QuizID)
        {
            string uri = "api/Rating?QuizID=" + QuizID.ToString();
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Rating>>(responseString);
            return respons;
        }
    }
}
