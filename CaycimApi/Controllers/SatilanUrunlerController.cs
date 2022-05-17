using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace CaycimApi.Controllers
{
    [Authorize(Roles = "Çaycı")]
    [RoutePrefix("api/SatilanUrunler")]
    public class SatilanUrunlerController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public List<SatilanUrunlerView> Get(string aralik)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            List<SatilanUrunlerView> satilanUrunlerList = new List<SatilanUrunlerView>();

            IQueryable<SepetSiparis> satilanUrunler;

            if (userId != null)
            {
                if (aralik.Contains("Yıllık"))
                {
                    satilanUrunler = context.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Year == DateTime.Now.Year)
                    .Include(p => p.SepetUruns)
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun))
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun.Urun));
                }
                else if (aralik.Contains("Aylık"))
                {
                    satilanUrunler = context.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year)
                    .Include(p => p.SepetUruns)
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun))
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun.Urun));
                }
                else
                {
                    satilanUrunler = context.SepetSiparis.Where(p => p.CayciId == userId && p.Tarih.Day == DateTime.Now.Day && p.Tarih.Month == DateTime.Now.Month && p.Tarih.Year == DateTime.Now.Year)
                    .Include(p => p.SepetUruns)
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun))
                    .Include(p => p.SepetUruns.Select(a => a.KullaniciUrun.Urun));
                }
                 
                if (satilanUrunler.Any())
                {
                    foreach(var siparis in satilanUrunler)
                    {
                        foreach(var sepetUrun in siparis.SepetUruns)
                        {
                            var deger = satilanUrunlerList.Where(p => p.UrunId == sepetUrun.KullaniciUrun.UrunId).FirstOrDefault();
                            if(deger != null)
                            {
                                deger.Adet += sepetUrun.Adet;
                                deger.ToplamFiyat += sepetUrun.Fiyat;
                            }
                            else
                            {
                                satilanUrunlerList.Add(new SatilanUrunlerView()
                                {
                                    UrunId = sepetUrun.KullaniciUrun.UrunId,
                                    UrunAdi = sepetUrun.KullaniciUrun.Urun.UrunAdi,
                                    Adet = sepetUrun.Adet,
                                    ToplamFiyat = sepetUrun.Fiyat
                                });
                            }
                        }
                    }
                }
            }
            satilanUrunlerList.Sort((a, b) => b.ToplamFiyat.CompareTo(a.ToplamFiyat));
            return satilanUrunlerList;
        }
    }
}