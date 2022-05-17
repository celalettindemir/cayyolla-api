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

namespace CaycimApi.Controllers
{
    [Authorize(Roles ="Müşteri")]
    [RoutePrefix("api/GecmisSiparisMusteri")]
    public class GecmisSiparisMusteriController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/values
        public List<SepetSiparis> Get()
        {

            var userId = RequestContext.Principal.Identity.GetUserId();
            List<SepetSiparis> siparis = new List<SepetSiparis>();
            if (userId != null)
                siparis = contex.SepetSiparis.Where(p => p.IsConfirm == false && p.MusteriId == userId).ToList();
            return siparis;
        }

        // GET api/values
        public SepetSiparis Get(int id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            SepetSiparis siparis = new SepetSiparis();
            if (userId != null)
                siparis = contex.SepetSiparis.Where(p => p.IsConfirm == false && p.MusteriId == userId && p.ID == id).FirstOrDefault();
            return siparis;
        }
    }

    [Authorize(Roles = "Çaycı")]
    [RoutePrefix("api/GecmisSiparisCayci")]
    public class GecmisSiparisCayciController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/values
        //GecmisSiparis 
        public List<SiparisCayciViewModel> Get()
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            List<SiparisCayciViewModel> siparisList = new List<SiparisCayciViewModel>();
            if (userId != null)
            {
                var siparis = contex.SepetSiparis.Include(p => p.Musteri).Include(a => a.SepetUruns)
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun))
                    .Where(p => p.IsConfirm == true && p.CayciId == userId);


                foreach (var sip in siparis)
                {
                    var deger = "";
                    var sepetUruns = sip.SepetUruns.ToList();
                    var n = 2;
                    var kesilecekNokta = 0;
                    foreach (var a in sepetUruns)
                    {
                        deger += a.Adet + " " + a.KullaniciUrun.Urun.UrunAdi + " - ";
                    }
                    siparisList.Add(new SiparisCayciViewModel()
                    {
                        Id = sip.ID.ToString(),
                        ToplamFiyat = sip.ToplamFiyat.ToString(),
                        MusteriName = sip.Musteri.CompanyName,
                        SiparisZaman = sip.Tarih.ToString("HH:mm:ss"),
                        SepetUrun = (deger.Remove((kesilecekNokta = deger.TakeWhile(c => (n -= (c == '-' ? 1 : 0)) > 0).Count()) == deger.Length ? deger.Length - 3 : kesilecekNokta - 1) + ((sepetUruns.Count > 2) ? " ..." : ""))
                        //SepetUrun = (deger.Remove((kesilecekNokta = deger.TakeWhile(c => (n -= (c == '-' ? 1 : 0)) > 0).Count()) == deger.Length ? kesilecekNokta : kesilecekNokta - 1) + ((sepetUruns.Count > 2) ? " ..." : ""))
                    });
                }
            }
            return siparisList;
            //List<ApplicationUser> userlist=  UserManager.Users;
            //return userList.ToList();
        }
        public SiparisCayciDetayListViewModel Get(int id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            SiparisCayciDetayListViewModel siparisList = new SiparisCayciDetayListViewModel();

            var siparisUrun = new List<SiparisCayciDetayViewModel>();
            if (userId != null)
            {
                var siparis = contex.SepetSiparis.Where(p => p.ID == id).Include(p => p.Musteri).Include(a => a.SepetUruns)
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun))
                    .Include(a => a.SepetUruns.Select(b => b.KullaniciUrun.Urun))
                    .Where(p => p.IsConfirm == true && p.CayciId == userId).FirstOrDefault();

                if (siparis == null) return new SiparisCayciDetayListViewModel();

                foreach (var a in siparis.SepetUruns)
                {
                    siparisUrun.Add(new SiparisCayciDetayViewModel {
                        Id=a.ID.ToString(),
                        UrunName = a.KullaniciUrun.Urun.UrunAdi,
                        Adet = a.Adet,
                        Fiyat=a.Fiyat
                    });
                }
                siparisList = new SiparisCayciDetayListViewModel()
                {
                    Id = id.ToString(),
                    MusteriName = siparis.Musteri.CompanyName,
                    Not =siparis.Not,
                    SiparisZaman = siparis.Tarih.ToString("HH:mm:ss"),
                    ToplamFiyat = siparis.ToplamFiyat.ToString(),
                    model= siparisUrun

                };
            }
            return siparisList;
            //List<ApplicationUser> userlist=  UserManager.Users;
            //return userList.ToList();
        }
    }
}