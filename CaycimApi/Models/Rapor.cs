using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{

    public class CayciRapor
    {
        [Key]
        public int ID { get; set; }
        public float ToplamTutar { get; set; }
        public float TahsilatTutar { get; set; }
        public string CayciId { get; set; }
        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
    }
    public class MusteriRapor
    {
        [Key]
        public int ID { get; set; }
        public float ToplamTutar { get; set; }
        public float OdenenTutar { get; set; }
        public string MusteriId { get; set; }
        [ForeignKey("MusteriId")]
        public ApplicationUser Musteri { get; set; }
    }
}