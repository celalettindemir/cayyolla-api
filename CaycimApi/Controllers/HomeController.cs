using CaycimApi.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaycimApi.Controllers
{
    public class HomeController : Controller
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
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult GetTokens()
        {
            var userList = UserManager.Users.ToList();
            var tokenList = new List<BearerTokenView>();
            foreach (var u in userList)
            {

                using (var httpClient = new HttpClient())
                {
                    var tokenRequest =
                        new List<KeyValuePair<string, string>>
                            {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", u.UserName),
                        new KeyValuePair<string, string>("password", "Qwer1234.")
                            };

                    HttpContent encodedRequest = new FormUrlEncodedContent(tokenRequest);
                    var response = httpClient.PostAsync(URL.host+"/Token", encodedRequest);
                    response.Wait();
                    var token = response.Result.Content.ReadAsAsync<BearerToken>();
                    token.Wait();
                    tokenList.Add(new BearerTokenView()
                    {
                        AccessToken = token.Result.AccessToken,
                        TokenType = token.Result.TokenType,
                        UserName = token.Result.UserName
                    });
                }
            }

            return Json(tokenList, JsonRequestBehavior.AllowGet);
        }
        
    }
}
