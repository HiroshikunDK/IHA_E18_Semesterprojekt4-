using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using Newtonsoft.Json;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        public async Task<List<Answer>> GetAnswerByQuestionID(long id)
        {
            string uri = "api/Answer?QuestionID=" + id;
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Answer>>(responseString);
            return respons;
        }
    }
}
