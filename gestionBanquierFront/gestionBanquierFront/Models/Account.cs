using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gestionBanquierFront.Models
{
    public class Account
    {


    
        public int id { get; set; }
        [Display(Name = "Account Number")]
        public int numCompte { get; set; }
        [DataType(DataType.Date)]
        public DateTime dateCreation { get; set; }
        public double solde { get; set; }
        public bool activated { get; set; }
        [Display(Name = "Account borrow money")]
        public bool accountIsInRedStated { get; set; }
        public int timesTheAccountReachNegativeSolde { get; set; }
        public string category { get; set; }

       
        public List<ListOfBorrowTime> listOfBorrowTimes { get; set; }


        public double maxAmountToBorrow { get; set; }

        [JsonIgnore]
        public User SelectedUser { get; set; }


    }

    public class ListOfBorrowTime
    {
        public int id { get; set; }
        public DateTime dateOfBorrow { get; set; }
        public double amount { get; set; }
    }
}