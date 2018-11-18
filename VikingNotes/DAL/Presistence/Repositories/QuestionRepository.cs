﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using RESTfullWebApi.Models;

namespace DAL.Presistence.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
    }
}
