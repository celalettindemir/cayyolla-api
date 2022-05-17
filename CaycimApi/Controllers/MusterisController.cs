using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaycimApi.Controllers
{
    [Authorize(Roles ="Çaycı")]
    public class MusterisController : ApiController
    {

        ApplicationDbContext contex = new ApplicationDbContext();
        // GET api/<controller>
        public IEnumerable<ApplicationUser> Get()
        {

            var userId = RequestContext.Principal.Identity.GetUserId();
            return contex.CayciMusteri.Where(p => p.CayciId == userId).Select(p => p.Musteri);
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
        }
    }
}