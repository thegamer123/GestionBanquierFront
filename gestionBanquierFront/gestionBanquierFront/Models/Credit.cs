using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
    public enum TypeClient { PARTICUIER, ENTRPRISE };
    public class Credit
    {

        public long id { get; set; }

        public int dureeCredit { get; set; }

        public int montant { get; set; }

        public float tauxInteret { get; set; }

        public string typeclt { get; set; }
        public float salaire { get; set; }

        [JsonIgnore]
        public Account account;

        [JsonIgnore]
        public virtual List<tableauAmortissement> tableauAmortissement { get; set; }


        public Credit()
        {

        }


    }
}