using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class Odeme
    {
        [Key]
        public int ID { get; set; }
        public int SepetSiparisId { get; set; }
        public int OdemeKullaniciId { get; set; }
        [ForeignKey("SepetSiparisId")]
        public SepetSiparis SepetSiparis { get; set; }
        [ForeignKey("OdemeKullaniciId")]
        public OdemeKullanici OdemeKullanici { get; set; }
    }
    public class OdemeKullanici
    {
        [Key]
        public int ID { get; set; }
        public string MusteriId { get; set; }
        public string CayciId { get; set; }
        public float ToplamFiyat { get; set; }

        public DateTime Tarih { get; set; }
        public bool IsConfirm { get; set; }

        [ForeignKey("MusteriId")]
        public ApplicationUser Musteri { get; set; }

        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
        public ICollection<Odeme> Odemes { get; set; }
    }
}