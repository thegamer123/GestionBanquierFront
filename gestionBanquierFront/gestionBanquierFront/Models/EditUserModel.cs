using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
    public class EditUserModel
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }


    }
}