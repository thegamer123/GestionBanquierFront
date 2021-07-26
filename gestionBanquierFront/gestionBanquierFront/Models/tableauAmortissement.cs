
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gestionBanquierFront.Models
{
    public enum Etat { PAYEE, IMPAYEE }
    public class tableauAmortissement
    {


        public long id { get; set; }
        public float Mensualite { get; set; }
        public float Amortissement { get; set; }
        public float Interts { get; set; }
        public float tauxInteret { get; set; }
        public float Annuitee { get; set; }
        public Etat Etat { get; set; }

        public Credit credit;

        public tableauAmortissement()
        {

        }
    }
}