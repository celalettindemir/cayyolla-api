using CaycimApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Data.Entity;
using CaycimApi.Utils;

namespace CaycimApi.Controllers
{
    [Authorize(Roles = "Müşteri")]
    [RoutePrefix("api/SiparisMusteri")]
    public class SiparisMusteriController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();

        [HttpPost]
        // POST api/values
        public async Task<IHttpActionResult> PostAsync(SiparisMusteriViewListModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var cayci = contex.CayciMusteri.Where(p => p.MusteriId == userId).Select(p => p.Cayci).Include(p => p.KullaniciUruns).FirstOrDefault();
            var cayciUrun = cayci.KullaniciUruns;
            var sepetSiparis = new SepetSiparis();
            sepetSiparis.MusteriId = userId;
            sepetSiparis.CayciId = cayci.Id;
            sepetSiparis.IsConfirm = false;
            sepetSiparis.Not = model.Not;
            sepetSiparis.Tarih = DateTime.Now;
            sepetSiparis.SepetUruns = new List<SepetUrun>();
            foreach (var i in model.model)
            {
                var kurun = cayciUrun.Where(p => p.ID == i.ID).FirstOrDefault();
                sepetSiparis.SepetUruns.Add(new SepetUrun()
                {
                    Fiyat = (kurun.Fiyat * i.count),
                    KullaniciUrunId = kurun.ID,
                    Adet = i.count,
                });
            }
            sepetSiparis.ToplamFiyat = sepetSiparis.SepetUruns.Sum(p => p.Fiyat);
            sepetSiparis.IsPaid = false;


            contex.SepetSiparis.Add(sepetSiparis);
            contex.SaveChanges();
            DateTime date = DateTime.Now.AddMinutes(5);
            int result = DateTime.Compare(cayci.lastUpdate, date);
            if (result < 0&&!String.IsNullOrEmpty(cayci.fcmToken))
                await PushNotificationLogic.SendPushNotification(new string[] { cayci.fcmToken }, contex.Users.Where(p=>p.Id==userId).First().CompanyName, "Beklemede Siparişiniz var...");
            return Ok(); // return collection of added products
        }
    }

    [Authorize(Roles = "Çaycı")]
    [RoutePrefix("api/SiparisCayci")]
    public class SiparisCayciController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/values
        public IEnumerable<SiparisBildirimCayciViewModel> Get()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            int counterID = 1;
            List<SiparisBildirimCayciViewModel> siparisList = new List<SiparisBildirimCayciViewModel>();
            var siparis = contex.SepetSiparis.Include(p => p.Musteri).Include(a => a.SepetUruns)
                .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun))
                .Where(p => p.IsConfirm == false && p.CayciId == userId);

            var musteriBidirim = contex.OdemeKullanici.Where(p => p.CayciId == userId && p.IsConfirm == false).Include(p => p.Musteri);

            foreach (var i in musteriBidirim)
            {
                siparisList.Add(new SiparisBildirimCayciViewModel()
                {
                    CounterId = counterID.ToString(),
                    Id = i.ID.ToString(),
                    IsSiparis = false,
                    MusteriName = i.Musteri.CompanyName,
                    Tarih = i.Tarih.ToString("HH:mm:ss"),
                    LongTarih = i.Tarih,
                    ToplamFiyat = i.ToplamFiyat.ToString()
                });
                counterID++;
            }
            foreach (var sip in siparis)
            {
                var deger = "";
                var sepetUruns = sip.SepetUruns.ToList();
                foreach (var a in sepetUruns.Take(2))
                {
                    deger += a.Adet + " " + a.KullaniciUrun.Urun.UrunAdi + " - ";
                }
                siparisList.Add(new SiparisBildirimCayciViewModel()
                {
                    CounterId = counterID.ToString(),
                    Id = sip.ID.ToString(),
                    Not=sip.Not,
                    IsSiparis = true,
                    ToplamFiyat = sip.ToplamFiyat.ToString(),
                    MusteriName = sip.Musteri.CompanyName,
                    Tarih = sip.Tarih.ToString("HH:mm:ss"),
                    LongTarih = sip.Tarih,
                    SepetUrun = (deger.Remove(deger.Length - 3) + ((sepetUruns.Count > 2) ? " ..." : ""))

                });
                counterID++;
            }
            return siparisList.OrderBy(p => p.LongTarih);
            //List<ApplicationUser> userlist=  UserManager.Users;
            //return userList.ToList();
        }
        public SiparisCayciDetayListViewModel Get(int id, bool IsSiparis)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            SiparisCayciDetayListViewModel siparisList = new SiparisCayciDetayListViewModel();

            if (IsSiparis)
            {
                var siparisUrun = new List<SiparisCayciDetayViewModel>();


                SepetSiparis siparis = contex.SepetSiparis.Where(p => p.ID == id).Include(p => p.Musteri).Include(a => a.SepetUruns)
                         .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                         .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun))
                         .Where(p => p.IsConfirm == false && p.CayciId == userId).FirstOrDefault();
                if (siparis == null) return siparisList;
                foreach (var a in siparis.SepetUruns)
                {
                    siparisUrun.Add(new SiparisCayciDetayViewModel
                    {
                        Id = a.ID.ToString(),
                        UrunName = a.KullaniciUrun.Urun.UrunAdi,
                        Adet = a.Adet,
                        Fiyat = a.Fiyat
                    });
                }
                siparisList = new SiparisCayciDetayListViewModel()
                {
                    Id = id.ToString(),
                    MusteriName = siparis.Musteri.CompanyName,
                    SiparisZaman = siparis.Tarih.ToString("HH:mm:ss"),
                    Not=siparis.Not,
                    ToplamFiyat = siparis.ToplamFiyat.ToString(),
                    model = siparisUrun

                };
            }
            else
            {
                var musteriBidirim = contex.OdemeKullanici.Where(p => p.CayciId == userId && p.ID == id && p.IsConfirm == false)
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns.Select(p => p.KullaniciUrun)))
                    .Include(a => a.Odemes.Select(b => b.SepetSiparis.SepetUruns.Select(p => p.KullaniciUrun.Urun)))
                    .Include(p => p.Musteri).FirstOrDefault();
                if (musteriBidirim == null) return siparisList;
                var siparisUrun = new List<SiparisCayciDetayViewModel>();

                foreach (var a in musteriBidirim.Odemes)
                    foreach (var b in a.SepetSiparis.SepetUruns)
                    {
                        var deger = siparisUrun.Where(p => p.Id == b.KullaniciUrunId.ToString()).FirstOrDefault();
                        if (deger != null)
                        {
                            deger.Adet += b.Adet;
                            deger.Fiyat += b.Fiyat;
                        }
                        else
                        {
                            siparisUrun.Add(new SiparisCayciDetayViewModel
                            {
                                Id = b.KullaniciUrunId.ToString(),
                                UrunName = b.KullaniciUrun.Urun.UrunAdi,
                                Adet = b.Adet,
                                Fiyat = b.Fiyat
                            });
                        }
                    }
                siparisList.Id = id.ToString();
                siparisList.MusteriName = musteriBidirim.Musteri.CompanyName;
                siparisList.SiparisZaman = musteriBidirim.Tarih.ToString("HH:mm:ss");
                siparisList.ToplamFiyat = musteriBidirim.ToplamFiyat.ToString();
                siparisList.model = siparisUrun;
            }
            return siparisList;
            //List<ApplicationUser> userlist=  UserManager.Users;
            //return userList.ToList();
        }


        [HttpPost]
        // POST api/values
        public IHttpActionResult Post(SiparisCayciDetayListConfirmViewModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var cayci = contex.Users.Where(p => p.Id == userId).FirstOrDefault();
            cayci.lastUpdate = DateTime.Now;
            var Id = Convert.ToInt32(model.Id);
            if (model.IsSiparis)
            {
                var siparis = contex.SepetSiparis.Where(p => p.ID == Id).FirstOrDefault();
                siparis.IsConfirm = true;
                /*Burası*/
            }
            else
            {
                var MusteriOdemeSip = contex.Odeme.Where(p => p.OdemeKullaniciId == Id).Include(p => p.SepetSiparis);
                foreach (var i in MusteriOdemeSip)
                {
                    i.SepetSiparis.IsPaid = true;
                }
                var OdemeKullanici = contex.OdemeKullanici.Where(p => p.ID == Id).FirstOrDefault();
                OdemeKullanici.IsConfirm = true;
            }
            contex.SaveChanges();
            return Ok(); // return collection of added products
        }
        // PUT api/values/5
        public void Put(int id, [FromBody]ApplicationUser value)
        {
        }

        // DELETE api/values/5
        public void Delete(string id)
        {

        }
    }
}