using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
 

    public class ListAccount
    {
        public int id { get; set; }
        public int numCompte { get; set; }
        public DateTime dateCreation { get; set; }
        public double solde { get; set; }
        public bool activated { get; set; }
        public bool accountIsInRedStated { get; set; }
        public int timesTheAccountReachNegativeSolde { get; set; }
        public string category { get; set; }
        public List<ListOfBorrowTime> listOfBorrowTimes { get; set; }
        public double maxAmountToBorrow { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string prenom { get; set; }
        public string nom { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int numTel { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateNaissance { get; set; }
        public double score { get; set; }
        public List<Role> roles { get; set; }
        public List<ListAccount> listAccount { get; set; }
    }
}