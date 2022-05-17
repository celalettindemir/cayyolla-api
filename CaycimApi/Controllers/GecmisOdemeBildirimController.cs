using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaycimApi.Controllers
{
    [RoutePrefix("api/GecmisOdemeBildirim")]
    public class GecmisOdemeBildirimController : ApiController
    {
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/

        ApplicationDbContext context = new ApplicationDbContext();
        [Authorize(Roles = "Çaycı")]
        public List<GecmisOdemeBildirimView> Get()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            List<GecmisOdemeBildirimView> gecmisOdemeBildirimList = new List<GecmisOdemeBildirimView>();

            if(userId != null)
            {
                var gecmisOdemeBildirimler = context.OdemeKullanici.Where(p => p.CayciId == userId).Include(p => p.Musteri);

                if(gecmisOdemeBildirimler.Any())
                {
                    foreach(var gecmisOdemeBildirim in gecmisOdemeBildirimler)
                    {
                        gecmisOdemeBildirimList.Add(new GecmisOdemeBildirimView()
                        {
                            Id = gecmisOdemeBildirim.ID.ToString(),
                            CompanyName = gecmisOdemeBildirim.Musteri.CompanyName,
                            Tarih = gecmisOdemeBildirim.Tarih,
                            ToplamFiyat = gecmisOdemeBildirim.ToplamFiyat.ToString()
                        });
                    }
                }
            }

            return gecmisOdemeBildirimList;
        }

        public GecmisOdemeDetayBildirimView Get(int Id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            GecmisOdemeDetayBildirimView gecmisOdemeDetayBildirim = new GecmisOdemeDetayBildirimView();

            if (userId != null)
            {
                //var gecmisOdemeBildirimSiparisler = context.OdemeKullanici.Where(p => p.ID.ToString() == Id)
                //    .Include(p => p.Musteri)
                //  .Include(p => p.Odemes.Select(a => a.SepetSiparis)).Where(c => c.Odemes.Any(a => a.OdemeKullaniciId.ToString() == Id))
                //  .Include(p => p.Odemes.Select(c => c.SepetSiparis.SepetUruns))
                //  .Include(p => p.Odemes.Select(d => d.SepetSiparis.SepetUruns))
                //  .Include(p => p.Musteri.KullaniciUruns)
                //  .Include(p => p.Musteri.KullaniciUruns.Select(c => c.Urun));


                var musteriBidirim = context.OdemeKullanici.Where(p => p.CayciId == userId && p.ID == Id && p.IsConfirm == true)
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns.Select(p => p.KullaniciUrun)))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns.Select(p => p.KullaniciUrun.Urun)))
                    .Include(p => p.Musteri).FirstOrDefault();
                if (musteriBidirim == null) return gecmisOdemeDetayBildirim;
                var siparisUrun = new List<OdemeBildirimUrunListViewModel>();

                foreach (var a in musteriBidirim.Odemes)
                    foreach (var b in a.SepetSiparis.SepetUruns)
                    {
                        var deger = siparisUrun.Where(p => p.UrunId == b.KullaniciUrun.UrunId).FirstOrDefault();
                        if (deger != null)
                        {
                            deger.Adet += b.Adet;
                            deger.ToplamFiyat += b.Fiyat;
                        }
                        else
                        {
                            siparisUrun.Add(new OdemeBildirimUrunListViewModel
                            {
                                UrunId = b.KullaniciUrun.UrunId,
                                UrunAdi = b.KullaniciUrun.Urun.UrunAdi,
                                Adet = b.Adet,
                                ToplamFiyat = b.Fiyat
                            });
                        }
                    }
                gecmisOdemeDetayBildirim.CompanyName = musteriBidirim.Musteri.CompanyName;
                gecmisOdemeDetayBildirim.ToplamFiyat = musteriBidirim.ToplamFiyat.ToString() ;
                gecmisOdemeDetayBildirim.BildirimUrunler = siparisUrun;
            }
            return gecmisOdemeDetayBildirim;
        }
    }
}