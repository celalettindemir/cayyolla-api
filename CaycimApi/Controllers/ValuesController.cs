using CaycimApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace CaycimApi.Controllers
{
    public class ValuesController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/values
        public string Get()
        {
            var urunList = contex.Urun.ToList();
            var list= "const Images = {";
            list+="\'nonIcon\':require(\'"+ "../assets/images/nonIcon.png" + "\'),";
            foreach (var p in urunList)
                list+="\'"+p.UrunAdi+"\':require(\'"+p.IconUrl+"\'),";
            list += "}";
            return list;
        }

        // GET api/values/5
        public DateTime Get(int id)
        {
            return DateTime.Now.AddDays(20);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
