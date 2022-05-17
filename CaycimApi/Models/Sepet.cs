using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class SepetSiparis
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Musteri")]
        public string MusteriId { get; set; }
        [ForeignKey("Cayci")]
        public string CayciId { get; set; }
        public float ToplamFiyat { get; set; }
        public string Not { get; set; }
        public DateTime Tarih { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsBildir { get; set; } = false;
        public bool IsGoruldu { get; set; } = false;
        public bool IsPaid { get; set; }

        [ForeignKey("MusteriId")]
        public ApplicationUser Musteri { get; set; }

        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
        public ICollection<SepetUrun> SepetUruns { get; set; }
        public ICollection<Odeme> Odemes { get; set; }
    }
    public class SepetUrun
    {
        [Key]
        public int ID { get; set; }
        public int SiparisId { get; set; }
        public int KullaniciUrunId { get; set; }
        public int Adet { get; set; }
        public float Fiyat { get; set; }

        [ForeignKey("SiparisId")]
        public SepetSiparis SepetSiparis { get; set; }
        [ForeignKey("KullaniciUrunId")]
        public KullaniciUrun KullaniciUrun { get; set; }
    }
}