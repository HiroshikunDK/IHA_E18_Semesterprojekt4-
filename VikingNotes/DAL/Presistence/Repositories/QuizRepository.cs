﻿using System;
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
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        public override async Task<Quiz> GetAsync(long id)
        {
            string uri = "api/Quiz/" + id;
            string responseString = await Client().GetStringAsync(uri);
            Quiz quiz = JsonConvert.DeserializeObject<Quiz>(responseString);

            uri = "api/Question?QuizID=" + id;
            responseString = await Client().GetStringAsync(uri);
            ICollection<Question> questions = JsonConvert.DeserializeObject<ICollection<Question>>(responseString);
            quiz.Questions = questions;

            //foreach (var question in quiz.Questions)
            //{
            //    uri = "api/Answer?QuestionID=" + question.QuestionID;
            //    responseString = await Client().GetStringAsync(uri);
            //    ICollection<Answer> answers = JsonConvert.DeserializeObject<ICollection<Answer>>(responseString);
            //    question.Answers = answers;
            //}
            return quiz;
        }
        public async Task<List<Quiz>> GetQuizzesByUserID(long UserID)
        {
            string uri = "api/Quiz?UserID=" + UserID.ToString();
            string responseString = await Client().GetStringAsync(uri);
            var respons = JsonConvert.DeserializeObject<List<Quiz>>(responseString);
            return respons;
        }

    }
}
