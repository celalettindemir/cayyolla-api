using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class AnaKategori
    {
        [Key]
        public int ID { get; set; }
        public string Adi { get; set; }
        public ICollection<Urun> Uruns { get; set; }
    }

}