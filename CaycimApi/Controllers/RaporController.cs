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

    
    [RoutePrefix("api/Rapor")]
    public class RaporController : ApiController
    {
        // GET api/<controller>

        ApplicationDbContext contex = new ApplicationDbContext();

        [Authorize(Roles = "Müşteri")]
        [Route("Musteri")]
        public MusteriRaporViewModel GetMusteri(string aralik)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            IQueryable<SepetSiparis> SepetSiparisleri;
            if (aralik.Contains("Yıllık"))
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.MusteriId == userId&&p.Tarih.Year==DateTime.Now.Year&& p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            else if (aralik.Contains("Aylık"))
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.MusteriId == userId && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year && p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            else
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.MusteriId == userId && p.Tarih.Day == DateTime.Now.Day && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year && p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            var musteriOdenmisSiparis = SepetSiparisleri
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun));

            var urunList = new List<RaporListViewModel>();
            foreach(var v1 in musteriOdenmisSiparis)
            {
                foreach (var v2 in v1.SepetUruns)
                {
                    var deger = urunList.Where(p => p.UrunId == v2.KullaniciUrunId).FirstOrDefault();
                    if(deger!=null)
                    {
                        deger.ToplamFiyat += v2.Fiyat;
                        deger.Adet += v2.Adet;
                    }
                    else
                    {
                        urunList.Add(new RaporListViewModel()
                        {
                            UrunId = v2.KullaniciUrunId,
                            UrunAdi = v2.KullaniciUrun.Urun.UrunAdi,
                            Adet = v2.Adet,
                            ToplamFiyat = v2.Fiyat
                        });
                    }
                }
            }

            var _urunList = urunList.OrderByDescending(p => p.Adet);
            var Musteri = new MusteriRaporViewModel()
            {
                OdenenTutar = SepetSiparisleri.Where(p => p.IsPaid == true).Count()>0? SepetSiparisleri.Where(p => p.IsPaid == true).Sum(p => p.ToplamFiyat):0,
                OdenecekTutar = SepetSiparisleri.Where(p => p.IsPaid == false && p.IsConfirm == true).Count()>0? SepetSiparisleri.Where(p => p.IsPaid == false).Sum(p => p.ToplamFiyat):0,
                ToplamTutar = SepetSiparisleri.Count() > 0 ? SepetSiparisleri.Sum(p => p.ToplamFiyat) : 0,
                TopUrun  = _urunList.Take(4),
                TopUrunDetay= _urunList
            };
            return Musteri;
        }

        [Authorize(Roles = "Çaycı")]
        [Route("Cayci")]
        public CayciRaporViewModel GetCayci(string aralik)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            IQueryable<SepetSiparis> SepetSiparisleri;
            if (aralik.Contains("Yıllık"))
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Year == DateTime.Now.Year && p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            else if (aralik.Contains("Aylık"))
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year && p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            else
            {
                SepetSiparisleri = contex.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Day == DateTime.Now.Day && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year && p.IsConfirm == true).Include(p => p.SepetUruns);
            }
            var musteriOdenmisSiparis = SepetSiparisleri
                    .Include(p => p.Musteri)
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun));


            var urunList = new List<RaporListViewModel>();
            var musteriList = new List<RaporUserListViewModel>();
            foreach (var v1 in musteriOdenmisSiparis)
            {
                var raporUser=musteriList.Where(p => p.ID == v1.MusteriId).FirstOrDefault();
                if(raporUser==null)
                {
                    raporUser = new RaporUserListViewModel()
                    {
                        ID = v1.MusteriId,
                        CompanyName = v1.Musteri.CompanyName,
                        Adet = 0
                    };
                    musteriList.Add(raporUser);
                }
                    
                foreach (var v2 in v1.SepetUruns)
                {
                    var deger = urunList.Where(p => p.UrunId == v2.KullaniciUrunId).FirstOrDefault();
                    if (deger != null)
                    {
                        deger.ToplamFiyat += v2.Fiyat;
                        deger.Adet += v2.Adet;
                        raporUser.Adet += v2.Adet;
                    }
                    else
                    {
                        urunList.Add(new RaporListViewModel()
                        {
                            UrunId = v2.KullaniciUrunId,
                            UrunAdi = v2.KullaniciUrun.Urun.UrunAdi,
                            Adet = v2.Adet,
                            ToplamFiyat = v2.Fiyat
                        });
                    }
                }
            }
            var _urunList = urunList.OrderByDescending(p => p.Adet);
            var Musteri = new CayciRaporViewModel()
            {
                TahsilatTutar = SepetSiparisleri.Where(p => p.IsPaid == true).Count() > 0 ? SepetSiparisleri.Where(p => p.IsPaid == true).Sum(p => p.ToplamFiyat) : 0,
                AlacakTutar = SepetSiparisleri.Where(p => p.IsPaid == false).Count() > 0 ? SepetSiparisleri.Where(p => p.IsPaid == false && p.IsConfirm == true).Sum(p => p.ToplamFiyat) : 0 ,
                ToplamTutar = SepetSiparisleri.Count() > 0 ? SepetSiparisleri.Sum(p => p.ToplamFiyat):0,
                TopMusteri = musteriList.OrderByDescending(p => p.Adet).Take(5),
                TopUrun = _urunList.Take(4),
                TopUrunDetay = _urunList
            };
            return Musteri;
        }
    }
}