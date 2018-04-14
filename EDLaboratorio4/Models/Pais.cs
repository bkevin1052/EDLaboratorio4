using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDLaboratorio4.Models
{
    public class Pais
    { 
        public string Nombre { get; set; }
        public List<Calcomania> Faltantes { get; set;}
        public List<Calcomania> Coleccionadas { get; set; }
        public List<Calcomania> DisponibleCambio { get; set; }
         public Pais()
        {
            Faltantes = new List<Calcomania>();
            Coleccionadas = new List<Calcomania>();
            DisponibleCambio = new List<Calcomania>();
        }
    }
}