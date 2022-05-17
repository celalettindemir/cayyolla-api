using CaycimApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using QRCoder;
using System.IO;
using System.Drawing;
using System;
using System.Drawing.Imaging;
using Microsoft.Owin.Security;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Text;
using System.Data.Entity;

namespace CaycimApi.Controllers
{
    public class KarekodController : ApiController
    {
        ApplicationDbContext contex = new ApplicationDbContext();
        private string GeneratedUniqueString(int deger)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets;

            int length = deger;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
        // GET api/values

        [Authorize(Roles ="Çaycı")]
        public string Get()
        {
            //ftp ile bağlan uploads klasörünü oluştur
            var userId = RequestContext.Principal.Identity.GetUserId();
            string code = GeneratedUniqueString(25);

            if (!contex.CayciKod.Any(p => p.CayciId == userId))
            {
                contex.CayciKod.Add(new CayciKod() { CayciId = userId, KarekodDeger = code });
                contex.SaveChanges();
            }
            var kayit = contex.CayciKod.Where(p => p.CayciId == userId).FirstOrDefault();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            string qrCodeImageAsBase64 = qrCode.GetGraphic(20);


            kayit.KarekodDeger = code;
            contex.SaveChanges();
            return qrCodeImageAsBase64;
        }

        [Authorize(Roles = "Müşteri")]
        [HttpPost] // added attribute
        public IHttpActionResult Post([FromBody] Karekod QRkod) // added FromBody as this is how you are sending the data
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var Cayci = contex.CayciKod.Where(p => p.KarekodDeger == QRkod.KarekodDeger).Select(p => p.Cayci).FirstOrDefault();
            if (!contex.CayciMusteri.Any(p => p.CayciId == Cayci.Id && p.MusteriId == userId))
            {
                contex.CayciMusteri.Add(new CayciMusteri() { CayciId = Cayci.Id, MusteriId = userId });
            }

            contex.SaveChanges();

            return Ok();
        }
    }
}