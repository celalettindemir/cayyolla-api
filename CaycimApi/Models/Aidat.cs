using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class KullaniciFatura
    {
        public int Id { get; set; }
        public string CayciId { get; set; }
        public float Ucret { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime SonOdemeTarihi { get; set; }
        public bool OdemeDurumu { get; set; }
        public DateTime? OdemeTarihi { get; set; }
        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
    }
    public class KullaniciAidat
    {
        public string ID { get; set; }
        public float Ucret { get; set; }
        public string AidatAralik { get; set; }
        [ForeignKey("ID")]
        [Required]
        public ApplicationUser Cayci { get; set; }
    }
}