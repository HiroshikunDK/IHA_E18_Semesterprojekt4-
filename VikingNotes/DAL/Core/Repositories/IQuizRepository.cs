﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfullWebApi.Models;

namespace DAL.Core.Repositories
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        new Task<Quiz> GetAsync(long id);
        Task<List<Quiz>> GetQuizzesByUserID(long UserID);
    }
}
