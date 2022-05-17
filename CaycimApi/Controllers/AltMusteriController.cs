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
    [RoutePrefix("api/AltMusteri")]
    public class AltMusteriController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET api/<controller>
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET api/<controller>/5
        /*public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<controller>
        /*public void Post([FromBody]string value)
        {
        }*/

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [Route("Hepsi")]
        [HttpGet]
        public List<AltMusteriViewModel> GetAll()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            List<AltMusteriViewModel> altMusteriList = new List<AltMusteriViewModel>();

            if (userId != null)
            {
                var altMusteriler = context.CayciMusteri.Where(p => p.CayciId == userId).Select(p => p.Musteri).Include(p => p.MusteriSepet)
                    .OrderBy(p => p.MusteriSepet.Any(b => b.IsPaid == false && b.IsConfirm == true) == false);

                if(altMusteriler.Any())
                {
                    foreach (var altMusteri in altMusteriler)
                    {
                        altMusteriList.Add(new AltMusteriViewModel()
                        {
                            Id = altMusteri.Id,
                            MusteriName = altMusteri.Name + " " + altMusteri.SurName,
                            CompanyName = altMusteri.CompanyName,
                            ToplamFiyat = altMusteri.MusteriSepet.Where(p => p.IsConfirm == true).Sum(p => p.ToplamFiyat).ToString(),
                            sonSiparis = altMusteri.MusteriSepet.Where(p => p.IsConfirm == true).Select(p => p.Tarih).LastOrDefault()
                        });
                    }
                }
            }
            return altMusteriList;

        }

        [Route("Borclu")]
        [HttpGet]
        public List<AltMusteriViewModel> GetBorclu()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            List<AltMusteriViewModel> altMusteriList = new List<AltMusteriViewModel>();

            if (userId != null)
            {
                //var altMusteriler = context.SepetSiparis.Where(p => p.IsConfirm == true && p.IsPaid == false).Select(p => p.)
                //    .Where(p => p.Cayci)
                var altMusteriler = context.CayciMusteri.Where(p => p.CayciId == userId).Select(p => p.Musteri).Include(p => p.MusteriSepet)
                    .Where(p => p.MusteriSepet.Any(a => a.IsConfirm == true && a.IsPaid == false));

                if (altMusteriler.Any())
                {
                    foreach (var altMusteri in altMusteriler)
                    {
                        altMusteriList.Add(new AltMusteriViewModel()
                        {
                            Id = altMusteri.Id,
                            MusteriName = altMusteri.Name + " " + altMusteri.SurName,
                            CompanyName = altMusteri.CompanyName,
                            ToplamFiyat = altMusteri.MusteriSepet.Where(p => p.IsConfirm == true && p.IsPaid == false).Sum(p => p.ToplamFiyat).ToString(),
                            sonSiparis = altMusteri.MusteriSepet.Where(p => p.IsConfirm == true && p.IsPaid == false).Select(p => p.Tarih).LastOrDefault()
                        });
                    }
                }
            }
            return altMusteriList;

        }
        
        [HttpGet]
        public AltMusteriDetayViewModel Get(string Id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            AltMusteriDetayViewModel altMusteriDetay = new AltMusteriDetayViewModel();

            if (userId != null)
            {
                var altMusteriSiparisler = context.SepetSiparis.Where(p => p.MusteriId == Id && p.CayciId == userId && p.IsConfirm == true && p.IsPaid == false)
                    .Include(p => p.Musteri)
                  .Include(p => p.SepetUruns)
                  .Include(p => p.SepetUruns.Select(c => c.KullaniciUrun))
                  .Include(p => p.SepetUruns.Select(c => c.KullaniciUrun).Select(d => d.Urun));

                var siparisUrun = new List<SiparisCayciDetayViewModel>();

                if (altMusteriSiparisler.Any())
                {
                    List<SiparisCayciDetayViewModel> siparisDetay = new List<SiparisCayciDetayViewModel>();
                    foreach (var sepetSiparis in altMusteriSiparisler)
                    {
                        foreach (var sepetUrun in sepetSiparis.SepetUruns)
                        {
                            var deger = siparisUrun.Where(p => p.Id == sepetUrun.KullaniciUrunId.ToString()).FirstOrDefault();
                            if (deger != null)
                            {
                                deger.Adet += sepetUrun.Adet;
                                deger.Fiyat += sepetUrun.Fiyat;
                            }
                            else
                            {
                                siparisDetay.Add(new SiparisCayciDetayViewModel()
                                {
                                    UrunName = sepetUrun.KullaniciUrun.Urun.UrunAdi,
                                    Adet = sepetUrun.Adet,
                                    Fiyat = sepetUrun.Fiyat,
                                    Id = sepetUrun.ID.ToString()
                                });
                            }
                        }
                    }

                    altMusteriDetay = new AltMusteriDetayViewModel()
                    {
                        MusteriName = altMusteriSiparisler.FirstOrDefault().Musteri.Name + " " + altMusteriSiparisler.FirstOrDefault().Musteri.SurName,
                        CompanyName = altMusteriSiparisler.FirstOrDefault().Musteri.CompanyName,
                        ToplamFiyat = altMusteriSiparisler.Sum(p => p.ToplamFiyat).ToString(),
                        siparisler = siparisDetay
                    };
                }
            }
            return altMusteriDetay;
        }

        [HttpPost]
        public IHttpActionResult Post(AltMusteriIdViewModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            OdemeKullanici odemeK;
            if (userId != null)
            {
                var siparisler = context.SepetSiparis.Where(p => p.MusteriId == model.Id && p.IsConfirm == true && p.IsPaid == false && p.CayciId == userId)
                    .Include(p => p.Musteri)
                    .Include(p => p.Cayci);
                foreach (var siparis in siparisler)
                {
                    context.OdemeKullanici.Add(odemeK = new OdemeKullanici()
                    {
                        MusteriId = siparis.MusteriId,
                        CayciId = siparis.CayciId,
                        ToplamFiyat = siparis.ToplamFiyat,
                        IsConfirm = true,
                        Tarih = siparis.Tarih
                    });

                    context.Odeme.Add(new Odeme()
                    {
                        SepetSiparisId = siparis.ID,
                        OdemeKullanici = odemeK
                    });
                    siparis.IsPaid = true;
                    siparis.IsBildir = true;
                }
                context.SaveChanges();
            }
            return Ok();
        }
    }
}