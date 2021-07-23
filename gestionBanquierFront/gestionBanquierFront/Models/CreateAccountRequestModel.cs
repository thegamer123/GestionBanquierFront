using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
    public class CreateAccountRequestModel
    {

        public string numCompte { get; set; }

        public string dateCreation { get; set; }
        public string solde { get; set; }
        public int activated { get; set; }

   
        public string maxAmountToBorrow { get; set; }
    }
}