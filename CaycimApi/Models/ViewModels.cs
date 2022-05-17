using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaycimApi.Models
{
    public class MenuMainContetViewModel
    {
        public string AnaKategori { get; set; }
        public List<MenuViewModel> data { get; set; }
    }
    public class MenuViewModel
    {
        public int ID { get; set; }
        public int UrunId { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public int count { get; set; }
    }
    public class MenuContetViewModel
    {
        public string Id { get; set; }
        public string title { get; set; }
        public List<MenuViewModel> data { get; set; }
    }
    public class MenuDuzenleViewModel
    {
        public int UrunId { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public bool isConfirm { get; set; }
    }
    public class MenuDuzenleContetViewModel
    {
        public int Id { get; set; }
        public string title { get; set; }
        public List<MenuDuzenleViewModel> data { get; set; }
    }
    public class SiparisMusteriViewModel
    {
        public int ID { get; set; }
        public int count { get; set; }
    }
    public class SiparisMusteriViewListModel
    {
        public IEnumerable<SiparisMusteriViewModel> model { get; set; }
        public string Not;
        public DateTime Tarih;
        //public string cayciName;

    }

    public class AltMusteriIdViewModel
    {
        public string Id;
        //public string cayciName;

    }

    public class MenuDuzenleViewListModel
    {
        public IEnumerable<MenuDuzenleViewModel> model { get; set; }
    }
    public class SiparisBildirimCayciViewModel
    {
        public string CounterId { get; set; }
        public string Id { get; set; }
        public string MusteriName { get; set; }
        public string SepetUrun { get; set; }
        public string ToplamFiyat { get; set; }
        public string Not { get; set; }
        public bool IsSiparis { get; set; }
        public DateTime LongTarih { get; set; }
        public string Tarih { get; set; }
    }
    public class SiparisCayciViewModel
    {
        public string Id { get; set; }
        public string MusteriName { get; set; }
        public string SepetUrun { get; set; }
        public string SiparisZaman { get; set; }
        public string ToplamFiyat { get; set; }
    }
    public class SiparisCayciDetayViewModel
    {
        public string Id { get; set; }
        public string UrunName { get; set; }
        public float Fiyat { get; set; }
        public int Adet { get; set; }
    }
    public class SiparisCayciDetayListConfirmViewModel
    {
        public string Id { get; set; }
        public bool IsSiparis { get; set; }
    }
    public class SiparisCayciDetayListViewModel
    {
        public string Id { get; set; }
        public IEnumerable<SiparisCayciDetayViewModel> model { get; set; }
        public string Not { get; set; }
        public string MusteriName { get; set; }
        public string SiparisZaman { get; set; }
        public string ToplamFiyat { get; set; }
    }

    public class AltMusteriViewModel
    {
        public string Id { get; set; }
        public string MusteriName { get; set; }
        public string CompanyName { get; set; }
        public string ToplamFiyat { get; set; }
        public DateTime sonSiparis { get; set; }
    }

    public class AltMusteriDetayViewModel
    {
        public string MusteriName { get; set; }
        public string CompanyName { get; set; }
        public string ToplamFiyat { get; set; }
        public IEnumerable<SiparisCayciDetayViewModel> siparisler { get; set; }
    }

    public class GecmisOdemeBildirimView
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string ToplamFiyat { get; set; }
        public DateTime Tarih { get; set; }
    }

    public class GecmisOdemeDetayBildirimView
    {
        public string CompanyName { get; set; }
        public string ToplamFiyat { get; set; }
        public IEnumerable<OdemeBildirimUrunListViewModel> BildirimUrunler { get; set; }
    }

    public class SatilanUrunlerView
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public float ToplamFiyat { get; set; }
        //public IEnumerable<OdemeBildirimUrunListViewModel> UrunList { get; set; }
    }
    public class BearerToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty(".issued")]
        public string Issued { get; set; }

        [JsonProperty(".expires")]
        public string Expires { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("kullaniciTur")]
        public string KullaniciTur { get; set; }

        [JsonProperty("cayOcagiAdi")]
        public string CayOcagiAdi { get; set; }
    }
    public class BearerTokenView
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string UserName { get; set; }
    }
   
    public class OdemeBildirimUrunListViewModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public float ToplamFiyat { get; set; }
    }
    public class GunlukHarcama
    {
        public DateTime gun { get; set; }
        public int Harcama { get; set; }
    }
    public class OdemeBildirimViewModel
    {
        public float ToplamTutar { get; set; }
        public IEnumerable<float> GunlukHarcama { get; set; } 
        public IEnumerable<OdemeBildirimUrunListViewModel> UrunList {get;set;}
    }
    public class CayciOdemeBildirimListViewModel
    {
        public int ID { get; set; }
        public string MusteriName { get; set; }
        public string ToplamFiyat { get; set; }
        public string Tarih { get; set; }
    }

    //OdecekTutar
    //OdenenTutar
    //ToplamTutar
    //Encok Alınan Ürünler 4  chart (adet adi)
    //Encok Alınan Ürünler (name adet fiyat)
    public class RaporListViewModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public float ToplamFiyat { get; set; }
    }
    public class RaporUserListViewModel
    {
        public string ID { get; set; }
        public string CompanyName { get; set; }
        public int Adet { get; set; }
    }
    public class MusteriRaporViewModel
    {
        public float OdenecekTutar { get; set; }
        public float OdenenTutar { get; set; }
        public float ToplamTutar { get; set; }

        public IEnumerable<RaporListViewModel> TopUrun { get; set; }
        public IEnumerable<RaporListViewModel> TopUrunDetay { get; set; }
    }
    public class CayciRaporViewModel
    {
        public float AlacakTutar { get; set; }
        public float TahsilatTutar { get; set; }
        public float ToplamTutar { get; set; }

        public IEnumerable<RaporListViewModel> TopUrun { get; set; }
        public IEnumerable<RaporUserListViewModel> TopMusteri { get; set; }
        public IEnumerable<RaporListViewModel> TopUrunDetay { get; set; }
    }
    public class CayciViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Enable { get; set; }
    }
    public class ResetPasswordModel
    {
        public string UserId { get; set; }
        public string ReturnToken { get; set; }
        public string Password { get; set; }
    }
}