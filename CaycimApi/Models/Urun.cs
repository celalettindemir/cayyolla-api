using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class Urun
    {
        [Key]
        public int ID { get; set; }
        public string UrunAdi { get; set; }
        public int AnaKategoriId { get; set; }
        public int Oncelik { get; set; }
        public string IconUrl { get; set; }
        [ForeignKey("AnaKategoriId")]
        public AnaKategori AnaKategori { get; set; }
    }
    public class KullaniciUrun
    {
        [Key]
        public int ID { get; set; }
        public string CayciId { get; set; }
        public int UrunId { get; set; }
        public float Fiyat { get; set; }
        public bool isPublish { get; set; }

        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }

        [ForeignKey("UrunId")]
        public Urun Urun { get; set; }

        public ICollection<SepetUrun> SepetUruns { get; set; }
    }
}