using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace CaycimApi.Controllers
{

    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private ApplicationUserManager _userManager;
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

        ApplicationDbContext contex = new ApplicationDbContext();

        [HttpGet]
        [Route("UserList")]
        public IEnumerable<ApplicationUser> GetUserList()
        {
            return UserManager.Users.ToList();
        }

        [HttpGet]
        [Route("CaycilarList")]
        public IEnumerable<CayciViewModel> GetCaycilarList()
        {
            var cayciRoleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var caycilar = UserManager.Users.Where(p => p.Roles.Any(a => a.RoleId == cayciRoleId));
            List<CayciViewModel> caycilarList = new List<CayciViewModel>();
            foreach(var cayci in caycilar)
            {
                caycilarList.Add(new CayciViewModel()
                {
                    Id = cayci.Id,
                    Name = cayci.Name,
                    SurName = cayci.SurName,
                    CompanyName = cayci.CompanyName,
                    Email = cayci.Email,
                    Gender = cayci.Gender,
                    Enable = cayci.Enable
                });
            }

            return caycilarList;
        }

        [HttpGet]
        [Route("User/{id}")]
        public UserAddViewModel GetUser(string id)
        {
            var _user = UserManager.Users.Where(p => p.Id == id).FirstOrDefault();
            if (_user == null) return new UserAddViewModel();
            var rolName = /*UserManager.GetRoles(_user.Id).SingleOrDefault() != null ? "Admin" : */UserManager.GetRoles(_user.Id).FirstOrDefault().ToString();
            return new UserAddViewModel()
            {
                Name=_user.Name,
                SurName=_user.SurName,
                UserName=_user.UserName,
                Email=_user.Email,
                CompanyName=_user.CompanyName,
                PhoneNumber=_user.PhoneNumber,
                GenderType=_user.Gender,
                Rol= rolName,
                Enable=false,
            };
        }
        [HttpPost]
        [Route("UserAdd")]
        public async Task<IHttpActionResult> UserAddAsync(UserAddViewModel user)
        {
            var _user = new ApplicationUser() { UserName = user.UserName, Email = user.Email, Name = user.Name, SurName = user.SurName, PhoneNumber = user.PhoneNumber, CompanyName = user.CompanyName, Gender = user.GenderType };
            await UserManager.CreateAsync(_user, user.Password);

            if (user.Rol.Contains("Admin"))
            {
                UserManager.AddToRole(_user.Id, "Admin");
            }
            else if (user.Rol.Contains("Çaycı"))
                UserManager.AddToRole(_user.Id, "Çaycı");
            else
                UserManager.AddToRole(_user.Id, "Müşteri");

            return Ok();
        }

        
        [HttpGet]
        [Route("Aidat")]
        public IEnumerable<AidatViewModel> GetAidatList()
        {
            var aidat = new List<AidatViewModel>();
            var aidatlar = contex.KullaniciAidat.Include(p => p.Cayci);
            foreach (var _aidat in aidatlar)
                aidat.Add(new AidatViewModel()
                {

                    ID = _aidat.ID,
                    Ucret = _aidat.Ucret,
                    AidatAralik = _aidat.AidatAralik,
                    CayciCompanyName = _aidat.Cayci.CompanyName
                });
            return aidat;
        }
        [HttpGet]
        [Route("Aidat/{id}")]
        public AidatViewModel GetAidat(string id)
        {
            var _aidat = contex.KullaniciAidat.Include(p=>p.Cayci).Where(p => p.ID == id).FirstOrDefault();
            if (_aidat == null) return new AidatViewModel();
            return new AidatViewModel()
            {
                ID= _aidat.ID,
                Ucret= _aidat.Ucret,
                AidatAralik= _aidat.AidatAralik,
                CayciCompanyName= _aidat.Cayci.CompanyName
                
            };
        }
        [HttpPut]
        [Route("Aidat/{id}")]
        public IHttpActionResult AidatGuncelle(string id, AidatViewModel aidat)
        {
            var _aidat = contex.KullaniciAidat.Where(p => p.ID ==id).Include(p => p.Cayci).SingleOrDefault();
            if (_aidat == null) return Content(HttpStatusCode.NotFound, "aidat bulunamadı");

            _aidat.Ucret = aidat.Ucret;
            _aidat.AidatAralik = aidat.AidatAralik;
            
            contex.SaveChanges();
            return Ok();
        }


        /*
        [HttpGet]
        [Route("CayciAidatsizGet")]
        public List<AidatViewModel> AidatsizGet()
        {
            var aidatsizKullanici = new List<AidatViewModel>();
            var roleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var allusers = contex.Users.Include(p=>p.KullaniciAidat).Include(p=>p.Roles).ToList();

            foreach (var user in allusers.Where(x => x.Roles.Select(p => p.RoleId).Contains(roleId)).ToList())
                if(user.KullaniciAidat==null)
                aidatsizKullanici.Add(new AidatViewModel()
                {
                    ID =user.Id,
                    CayciCompanyName =user.CompanyName,
                    AidatAralik = null,
                    Ucret = 0
                }
                );
            return aidatsizKullanici;
        }
        [HttpGet]
        [Route("CayciAidatsizGet/{id}")]
        public AidatViewModel AidatsizGet(string id)
        {
            var roleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var user = contex.Users.Include(p => p.KullaniciAidat).Include(p => p.Roles).Where(p => p.Id == id && p.KullaniciAidat == null).FirstOrDefault();

            if (user == null) return new AidatViewModel();

            return new AidatViewModel()
            {
                ID = user.Id,
                CayciCompanyName = user.CompanyName,
                AidatAralik = null,
                Ucret = 0
            };
        }*/
        [HttpGet]
        [Route("GetAidatAdd")]
        public List<AidatsizCayciViewModel> GetAidatAdd()
        {
            var aidatsizKullanici = new List<AidatsizCayciViewModel>();
            var roleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var allusers = contex.Users.Include(p => p.KullaniciAidat).Include(p => p.Roles).ToList();

            foreach (var user in allusers.Where(x => x.Roles.Select(p => p.RoleId).Contains(roleId)).ToList())
                if (user.KullaniciAidat == null)
                    aidatsizKullanici.Add(new AidatsizCayciViewModel()
                    {
                        ID = user.Id,
                        CompanyNameAndPhone = user.PhoneNumber + " - " +  user.CompanyName 
                    }
                    );
            return aidatsizKullanici;
        }

        [HttpPost]
        [Route("AidatAdd")]
        public IHttpActionResult AidatAdd(AidatViewModel _aidat)
        {
            var _user = contex.Users.Where(p => p.Id == _aidat.ID).FirstOrDefault();
            if (_user == null)
                return BadRequest();
            _user.KullaniciAidat = new KullaniciAidat()
            {
                Ucret = _aidat.Ucret,
                AidatAralik = _aidat.AidatAralik,
            };
            contex.KullaniciFatura.Add(new KullaniciFatura()
            {
                CayciId=_user.Id,
                Ucret = _aidat.Ucret,
                OlusturulmaTarihi = DateTime.Now,
                SonOdemeTarihi = DateTime.Now,
                OdemeDurumu = false
            });
            contex.SaveChanges();
            return Ok();
        }



        [HttpGet]
        [Route("CayciFaturaList")]
        public IEnumerable<FaturaViewModel> GetCayciFaturaList()
        {
            var faturalarList = new List<FaturaViewModel>();
            var faturalar = contex.KullaniciFatura.Include(p => p.Cayci);
            foreach (var fatura in faturalar)
            {
                if(fatura.SonOdemeTarihi<DateTime.Now.AddDays(10))
                faturalarList.Add(new FaturaViewModel()
                {
                    Id = fatura.Id,
                    CompanyName = fatura.Cayci.CompanyName,
                    Name = fatura.Cayci.Name,
                    SurName = fatura.Cayci.SurName,
                    OlusturulmaTarihi = fatura.OlusturulmaTarihi,
                    SonOdemeTarihi = fatura.SonOdemeTarihi,
                    OdemeTarihi = fatura.OdemeTarihi,
                    Ucret = fatura.Ucret,
                    OdemeDurumu = fatura.OdemeDurumu,
                    PhoneNumber = fatura.Cayci.PhoneNumber

                });
            }
            return faturalarList.OrderBy(p => p.OdemeDurumu).OrderByDescending(p => p.SonOdemeTarihi);
        }
        [HttpGet]
        [Route("CayciFatura/{id}")]
        public FaturaViewModel GetFatura(string id)
        {
            var fatura = contex.KullaniciFatura.Include(p => p.Cayci).Where(p => p.Id.ToString() == id).FirstOrDefault();
            if (fatura == null) return new FaturaViewModel();
            return new FaturaViewModel()
            {
                Id = fatura.Id,
                CompanyName = fatura.Cayci.CompanyName,
                Name = fatura.Cayci.Name,
                SurName = fatura.Cayci.SurName,
                OlusturulmaTarihi = fatura.OlusturulmaTarihi,
                SonOdemeTarihi = fatura.SonOdemeTarihi,
                OdemeTarihi = fatura.OdemeTarihi,
                Ucret = fatura.Ucret,
                OdemeDurumu = fatura.OdemeDurumu,
                PhoneNumber = fatura.Cayci.PhoneNumber
            };
        }

        [HttpPost]
        [Route("CayciFaturaOdeme/{id}")]
        public IHttpActionResult FaturaOde(string id)
        {
            var fatura = contex.KullaniciFatura.Where(p => p.Id.ToString() == id && p.OdemeDurumu == false).Include(p => p.Cayci.KullaniciAidat).FirstOrDefault();
            if (fatura == null)
                return Content(HttpStatusCode.NotFound, "Fatura bulunamadı");
            fatura.OdemeDurumu = true;
            fatura.OdemeTarihi = DateTime.Now;

            fatura.Cayci.Enable = true;
            contex.KullaniciFatura.Add(new KullaniciFatura()
            {
                CayciId = fatura.CayciId,
                Ucret = fatura.Ucret,
                OlusturulmaTarihi = fatura.Cayci.KullaniciAidat.AidatAralik.Contains("Yıllık") ? fatura.SonOdemeTarihi.AddMonths(11).AddDays(20) : fatura.SonOdemeTarihi.AddDays(20),
                SonOdemeTarihi = fatura.Cayci.KullaniciAidat.AidatAralik.Contains("Yıllık") ? fatura.SonOdemeTarihi.AddYears(1) : fatura.SonOdemeTarihi.AddMonths(1),
                OdemeDurumu = false
            });
            contex.SaveChanges();

            return Ok();

        }
        [HttpGet]
        [Route("CayciUrunler/{id}")]
        public List<CayciUrunlerViewModel> getCayciUrunler(string id)
        {
            var kullaniciUrunler = contex.KullaniciUrun.Where(p => p.CayciId == id).Include(p => p.Urun);

            List<CayciUrunlerViewModel> kullaniciUrunList = new List<CayciUrunlerViewModel>();
            foreach(KullaniciUrun kullaniciUrun in kullaniciUrunler)
            {
                kullaniciUrunList.Add(new CayciUrunlerViewModel()
                {
                    Id = kullaniciUrun.ID,
                    UrunId = kullaniciUrun.UrunId,
                    UrunIsim = kullaniciUrun.Urun.UrunAdi,
                    Fiyat = kullaniciUrun.Fiyat,
                    IsPublish = kullaniciUrun.isPublish,
                });
            }
            return kullaniciUrunList;
        }

        [HttpGet]
        [Route("CayciUrun/{kullaniciUrunId}")]
        public CayciUrunlerViewModel getCayciUrun(string kullaniciUrunId)
        {
            var kullaniciUrun = contex.KullaniciUrun.Where(p => p.ID.ToString() == kullaniciUrunId).Include(p => p.Urun).FirstOrDefault();
            if (kullaniciUrun == null) return new CayciUrunlerViewModel();
            return new CayciUrunlerViewModel()
            {
                Id = kullaniciUrun.ID,
                UrunId = kullaniciUrun.UrunId,
                UrunIsim = kullaniciUrun.Urun.UrunAdi,
                Fiyat = kullaniciUrun.Fiyat,
                IsPublish = kullaniciUrun.isPublish,
            };
        }

        [HttpPost]
        [Route("CayciUrunGuncelle")]
        public IHttpActionResult cayciUrunGuncelle(CayciUrunViewModel urun)
        {
            var kullaniciUrun = contex.KullaniciUrun.Where(p => p.UrunId == urun.UrunId && p.CayciId == urun.CayciId).Include(p => p.Urun).FirstOrDefault();

            if (kullaniciUrun == null) return Content(HttpStatusCode.NotFound, "Çaycı/Ürün bulunamadı");

            kullaniciUrun.Fiyat = urun.Fiyat;
            kullaniciUrun.isPublish = urun.IsPublish;

            contex.SaveChanges();

            return Ok();
        }
        [HttpPost]
        [Route("CayciEnable/{id}")]
        public IHttpActionResult CayciEnable(string id)
        {
            var cayciRoleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var cayci = contex.Users.Where(p => p.Id == id && p.Roles.Any(a => a.RoleId == cayciRoleId)).FirstOrDefault();

            if (cayci == null) return Content(HttpStatusCode.NotFound, "Çaycı bulunamadı");

            cayci.Enable = true;
            contex.SaveChanges();
            return Ok();

        }

        [HttpPost]
        [Route("CayciDisable/{id}")]
        public IHttpActionResult CayciDisable(string id)
        {
            var cayciRoleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var cayci = contex.Users.Where(p => p.Id == id && p.Roles.Any(a => a.RoleId == cayciRoleId)).FirstOrDefault();

            if (cayci == null) return Content(HttpStatusCode.NotFound, "Çaycı bulunamadı");

            cayci.Enable = false;
            contex.SaveChanges();
            return Ok();

        }

        /*
        [HttpPut]
        [Route("CayciAidat/{id}")]
        public AidatViewModel FaturaGuncelle(string id, AidatViewModel aidat)
        {
            var _aidat = contex.KullaniciAidat.Include(p => p.Cayci).Where(p => p.ID == id).FirstOrDefault();
            return new AidatViewModel()
            {
                ID = _aidat.ID,
                Ucret = _aidat.Ucret,
                AidatAralik = _aidat.AidatAralik,
                CayciCompanyName = _aidat.Cayci.CompanyName

            };
        }

        [HttpGet]
        [Route("CayciAdd")]
        public List<AidatAddUserViewModel> FaturaAdd()
        {
            var aidatsizKullanici = new List<AidatAddUserViewModel>();
            var roleId = contex.Roles.Where(p => p.Name == "Çaycı").FirstOrDefault().Id;
            var allusers = contex.Users.Include(p => p.KullaniciAidat).Include(p => p.Roles).ToList();

            foreach (var user in allusers.Where(x => x.Roles.Select(p => p.RoleId).Contains(roleId)).ToList())
                if (user.KullaniciAidat == null)
                    aidatsizKullanici.Add(new AidatAddUserViewModel() { Id = user.Id, CompanyName = user.CompanyName });
            return aidatsizKullanici;
        }
        [HttpPost]
        [Route("CayciAdd")]
        public IHttpActionResult FaturaAdd(AidatViewModel _aidat)
        {
            var _user = contex.Users.Where(p => p.Id == _aidat.ID).FirstOrDefault();
            contex.KullaniciAidat.Add(new KullaniciAidat()
            {
                Cayci = _user,
                Ucret = _aidat.Ucret,
                AidatAralik = _aidat.AidatAralik,

            });
            contex.KullaniciFatura.Add(new KullaniciFatura()
            {
                Cayci = _user,
                Ucret = _aidat.Ucret,
                OlusturulmaTarihi = DateTime.Now,
                SonOdemeTarihi = DateTime.Now.AddDays(1),
                OdemeDurumu = false
            });
            contex.SaveChanges();
            return Ok();
        }*/
    }
}