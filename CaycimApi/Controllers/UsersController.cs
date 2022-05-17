using CaycimApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace CaycimApi.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
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
        // GET api/values
        public ApplicationUser Get()
        {

            //List<ApplicationUser> userList = UserManager.Users.ToList();
            //List<ApplicationUser> userlist=  UserManager.Users;
            var userId = RequestContext.Principal.Identity.GetUserId();
            var user = contex.Users.Where(p => p.Id == userId).FirstOrDefault();
            return user;
        }

        [HttpPost]
        // POST api/values
        public IHttpActionResult Post(UpdateUserBindingModel model)
        {

            //var user = new ApplicationUser() { UserName = model.Email, Email = model.Email, Name = model.Name, SurName = model.Surname, PhoneNumber = model.Phone, CompanyName = model.CompanyName, Gender = model.Gender, KullaniciTurId = 1 };

            //IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            /*if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var user = contex.Users.Where(p => p.Id == userId).FirstOrDefault();
            if (!string.IsNullOrEmpty(model.Password))
            {
                if (UserManager.CheckPassword(user, model.Password))
                    UserManager.ChangePassword(user.Id, model.Password, model.NewPassword);
                else
                    return BadRequest(ModelState);
            }

            user.Name = model.Name;
            user.SurName = model.Surname;
            user.PhoneNumber = model.Phone;
            user.Gender = model.Gender;
            user.CompanyName = model.CompanyName;
            user.Email = model.Email;
            contex.SaveChanges();

            return Ok();

        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            if (id == null)
            {
                if (User.Identity.GetUserId() != id.ToString())
                {

                    var result = UserManager.DeleteAsync(UserManager.Users.Where(m => m.Id == id).FirstOrDefault());
                    //ViewBag.sonuc = "Kaldirma Başarili";
                }
                else
                {
                    //ViewBag.sonuc = "Kaldirma Başarısız.";
                }

            }
        }
    }
}