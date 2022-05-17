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

    [Authorize(Roles = "Çaycı")]
    public class CayciMenuController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/values
        public IEnumerable<MenuDuzenleContetViewModel> Get()
        {

            var categori = new List<MenuDuzenleContetViewModel>();
            var userId = RequestContext.Principal.Identity.GetUserId();
            var turId = contex.Roles;
            var CayciUrunler = contex.KullaniciUrun.Where(p => p.CayciId == userId).OrderBy(p => p.UrunId).ToList();
            var Category = contex.AnaKategori.Include(p => p.Uruns).ToList();
            foreach (var a in Category)
            {
                var menum = new List<MenuDuzenleViewModel>();
                foreach (var b in a.Uruns.OrderBy(p => p.Oncelik))
                {

                    var deger = CayciUrunler.Where(p => p.UrunId == b.ID).FirstOrDefault();
                    if (deger == null)
                    {
                        menum.Add(new MenuDuzenleViewModel()
                        {
                            title = b.UrunAdi,
                            UrunId = b.ID,
                            price = 0,
                            isConfirm = false

                        });
                    }
                    else
                    {
                        menum.Add(new MenuDuzenleViewModel()
                        {
                            title = b.UrunAdi,
                            UrunId = b.ID,
                            price = deger.Fiyat,
                            isConfirm = deger.isPublish ? true : false

                        });
                    }
                }
                categori.Add(
                        new MenuDuzenleContetViewModel()
                        {
                            Id = a.ID,
                            title = a.Adi,
                            data = menum
                        }
                    );
            }
            return categori;
        }


        [HttpPost]
        // POST api/values
        public IHttpActionResult Post(MenuDuzenleViewListModel model)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var MenuDuzenleViewModel = model.model;

            var CayciUrunler = contex.KullaniciUrun.Where(p => p.CayciId == userId).ToList();
            foreach(var a in CayciUrunler)
            {
                a.isPublish = false;
            }


            foreach (var a in MenuDuzenleViewModel)
            {
                var deger = CayciUrunler.Where(p => p.UrunId == a.UrunId).FirstOrDefault();

                if (deger == null)
                {
                    if (a.isConfirm)
                    {
                        contex.KullaniciUrun.Add(new KullaniciUrun()
                        {
                            CayciId = userId,
                            Fiyat = a.price,
                            UrunId = a.UrunId,
                            isPublish = true
                        });
                    }
                    else continue;
                }
                else
                {
                    if (!a.isConfirm)
                    {
                        deger.isPublish = false;
                    }
                    else
                    {
                        deger.Fiyat = a.price;
                        deger.isPublish = true;
                    }
                }
            }

            contex.SaveChanges();

            return Ok(); // return collection of added products
        }
    }

    [Authorize(Roles = "Müşteri")]
    public class MusteriMenuController : ApiController
    {

        private ApplicationUserManager _userManager;

        ApplicationDbContext contex = new ApplicationDbContext();
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET api/values
        public IEnumerable<MenuMainContetViewModel> Get()
        {
            var categori = new List<MenuMainContetViewModel>();
            var userId = RequestContext.Principal.Identity.GetUserId();
            ApplicationUser user = contex.CayciMusteri.Where(p => p.MusteriId == userId).Select(p => p.Cayci).FirstOrDefault();
            if (user != null)
            {
                var CayciUrunler = contex.KullaniciUrun.Where(p => p.CayciId == user.Id && p.isPublish == true).Include(p => p.Urun).Include(p => p.Urun.AnaKategori).OrderBy(p => p.Urun.Oncelik).ToList();
                //var KategoriUrun = CayciUrunler.Select(p => p.Urun.AltKategori.AnaKategori.Adi);
                foreach (var a in contex.AnaKategori)
                {
                    var menum = new List<MenuViewModel>();
                    foreach (var c in CayciUrunler.Where(c => c.Urun.AnaKategori.Adi == a.Adi))
                    {
                        menum.Add(new MenuViewModel()
                        {
                            ID = c.ID,
                            UrunId = c.UrunId,
                            title = c.Urun.UrunAdi,
                            price = c.Fiyat.ToString(),
                            count = 0,
                        });
                    }
                    categori.Add(
                            new MenuMainContetViewModel()
                            {
                                AnaKategori = a.Adi,
                                data = menum
                            }
                        );
                }
            }
            return categori;
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}