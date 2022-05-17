using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaycimApi.Controllers
{


    [RoutePrefix("api/OdemeBildirim")]
    public class OdemeBildirimController : ApiController
    {
        // GET api/<controller>

        ApplicationDbContext contex = new ApplicationDbContext();

        [Authorize(Roles = "Müşteri")]
        [Route("OdemeList")]
        public OdemeBildirimViewModel GetOdemeList()
        {

            //System.Diagnostics.Debugger.Break();
            var userId = RequestContext.Principal.Identity.GetUserId();
            
            if (contex.SepetSiparis.Where(p=>p.MusteriId==userId&&p.IsConfirm==true).All(p=>p.IsBildir==true))
                return new OdemeBildirimViewModel()
                {
                    GunlukHarcama = new List<float>(){ 0 },
                    UrunList = new List<OdemeBildirimUrunListViewModel>()
                    {
                        new OdemeBildirimUrunListViewModel()
                        {
                            UrunAdi="",
                            Adet=0,
                            UrunId=0,
                            ToplamFiyat=0
                        }
                    },
                    ToplamTutar = 0
                };

            var musteriOdenmemisSiparis = contex.SepetSiparis.Where(p => p.MusteriId == userId && p.IsPaid == false&&p.IsBildir==false && p.IsConfirm == true).Include(p => p.SepetUruns)
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun));
            var urunList = new List<OdemeBildirimUrunListViewModel>();
            var gunlukHarcama = new Dictionary<DateTime, float>();
            foreach (var v1 in musteriOdenmemisSiparis)
            {
                foreach (var v2 in v1.SepetUruns)
                {
                    //Burada döngü içerisinde başka bir query'nin sonuçları üzerinde giderken yeni query'de bulunulduğundan bazen hata alınabilir
                    //There is already an open DataReader associated with this Command which must be closed first
                    var deger = urunList.Where(p => p.UrunId == v2.KullaniciUrun.UrunId).FirstOrDefault();
                    if (deger != null)
                    {
                        deger.ToplamFiyat += v2.Fiyat;
                        deger.Adet += v2.Adet;
                    }
                    else
                    {
                        urunList.Add(new OdemeBildirimUrunListViewModel()
                        {
                            UrunId = v2.KullaniciUrun.UrunId,
                            UrunAdi = v2.KullaniciUrun.Urun.UrunAdi,
                            Adet = v2.Adet,
                            ToplamFiyat = v2.Fiyat
                        });
                    }
                }
                if (gunlukHarcama.ContainsKey(v1.Tarih.Date))
                    gunlukHarcama[v1.Tarih.Date] += v1.ToplamFiyat;
                else
                    gunlukHarcama[v1.Tarih.Date] = v1.ToplamFiyat;
            }
            gunlukHarcama.OrderBy(p => p.Key);
            var gunlukList = new List<float>();
            foreach (var i in gunlukHarcama)
                gunlukList.Add(i.Value);
            var value = new OdemeBildirimViewModel()
            {
                GunlukHarcama = gunlukList,
                UrunList = urunList.OrderByDescending(p => p.Adet),
                ToplamTutar = urunList.Sum(p => p.ToplamFiyat)
            };
            return value;
        }

        [Authorize(Roles = "Müşteri")]
        [Route("OdemeBildir")]
        public IHttpActionResult GetOdemeBildir()
        {
            //Aynı sipariş(ler) için her çağırdığında odemeKullanicis tablosuna (ve Odemes tablosuna) yeni satır giriliyor.
            //Bu istenen bir olay mı?
            //System.Diagnostics.Debugger.Break();
            var userId = RequestContext.Principal.Identity.GetUserId();
            var musteriOdenmemisSiparis = contex.SepetSiparis.Where(p => p.MusteriId == userId && p.IsPaid == false && p.IsBildir == false && p.IsConfirm == true);
            if (musteriOdenmemisSiparis.FirstOrDefault() != null)
            {
                var OdemeBildirimi = new OdemeKullanici()
                {
                    MusteriId = userId,
                    CayciId = musteriOdenmemisSiparis.FirstOrDefault().CayciId,
                    Tarih = DateTime.Now,
                    ToplamFiyat = musteriOdenmemisSiparis.Sum(p => p.ToplamFiyat),
                    IsConfirm = false
                };
                var Odeme = new List<Odeme>();
                foreach (var v1 in musteriOdenmemisSiparis)
                {
                    Odeme.Add(new Odeme()
                    {
                        OdemeKullanici = OdemeBildirimi,
                        SepetSiparis = v1,
                    });
                    v1.IsBildir = true;
                }
                contex.Odeme.AddRange(Odeme);
                contex.SaveChanges();
            }
            return Ok();
        }
        [Authorize(Roles = "Çaycı")]
        [Route("OdemeBildirimList")]
        public IEnumerable<CayciOdemeBildirimListViewModel> GetOdemeBildirimList()
        {

            //System.Diagnostics.Debugger.Break();
            var userId = RequestContext.Principal.Identity.GetUserId();
            var musteriBidirim = contex.OdemeKullanici.Where(p => p.CayciId == userId && p.IsConfirm == false).Include(p => p.Musteri);
            var odemeList = new List<CayciOdemeBildirimListViewModel>();
            foreach (var i in musteriBidirim)
                odemeList.Add(new CayciOdemeBildirimListViewModel()
                {
                    ID = i.ID,
                    MusteriName = i.Musteri.CompanyName,
                    Tarih = i.Tarih.ToString("dd/MM/yyyy HH:mm"),
                    ToplamFiyat = i.ToplamFiyat.ToString()
                });
            return odemeList;
        }
        //api/OdemeBildirim/OdemeOnayla?id=
        [Authorize(Roles = "Çaycı")]
        [Route("OdemeOnayla")]
        [HttpGet]
        public IHttpActionResult GetOdemeOnayla(int id)
        {

            //System.Diagnostics.Debugger.Break();
            var userId = RequestContext.Principal.Identity.GetUserId();
            var MusteriOdemeSip = contex.Odeme.Where(p => p.OdemeKullaniciId == id).Include(p => p.SepetSiparis);
            foreach (var i in MusteriOdemeSip)
            {
                i.SepetSiparis.IsPaid = true;
            }
            var OdemeKullanici = contex.OdemeKullanici.Where(p => p.ID == id).FirstOrDefault();
            OdemeKullanici.IsConfirm = true;
            contex.SaveChanges();
            return Ok();
        }
    }
}