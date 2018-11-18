using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTfullWebApi.Models
{
    public partial class UserrDTO
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        public long UserTypeID { get; set; }
        public long StudyID { get; set; }
        public string StudentNumber { get; set; }
        public string AuthToken { get; set; }
    }
}