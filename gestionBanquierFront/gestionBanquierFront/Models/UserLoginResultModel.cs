using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
    public class UserLoginResultModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public List<string> roles { get; set; }
        public string tokenType { get; set; }
        public string accessToken { get; set; }
    }
}