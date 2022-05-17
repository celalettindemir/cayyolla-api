using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using CaycimApi.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CaycimApi.Models
{

    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string CompanyName { get; set; }
        public bool Enable { get; set; } = false;
        public string Gender { get; set; }
        public string fcmToken { get; set; }

        public DateTime lastUpdate { get; set; } = DateTime.Now;

        [Index(IsUnique = true)]
        [StringLength(11)]
        public override string PhoneNumber { get; set; }
        
        public KullaniciAidat KullaniciAidat { get; set; }


        [ForeignKey("MusteriId")]
        public ICollection<CayciMusteri> Musteris { get; set; }
        [ForeignKey("CayciId")]
        public ICollection<CayciMusteri> Caycis { get; set; }
        [ForeignKey("CayciId")]
        public ICollection<OdemeKullanici> OdemeCaycis { get; set; }
        [ForeignKey("MusteriId")]
        public ICollection<OdemeKullanici> OdemeMusteris { get; set; }
        public ICollection<KullaniciUrun> KullaniciUruns { get; set; }
        public ICollection<CayciRapor> CayciRapors { get; set; }
        public ICollection<MusteriRapor> MusteriRapors { get; set; }

        [ForeignKey("CayciId")]
        public ICollection<SepetSiparis> CayciSepet { get; set; }
        [ForeignKey("MusteriId")]
        public ICollection<SepetSiparis> MusteriSepet { get; set; }

        [ForeignKey("CayciId")]
        public ICollection<KullaniciFatura> KullaniciFaturas { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CayciKod
    {
        [Key]
        public int ID { get; set; }
        public string CayciId { get; set; }
        public string KarekodDeger { get; set; }

        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
    }
    public class CayciMusteri
    {
        public int Id { get; set; }

        [ForeignKey("Musteri")]
        public string MusteriId { get; set; }

        [ForeignKey("MusteriId")]
        public ApplicationUser Musteri { get; set; }

        [ForeignKey("Cayci")]
        public string CayciId { get; set; }
        [ForeignKey("CayciId")]
        public ApplicationUser Cayci { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new Configuration());
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<CayciMusteri> CayciMusteri { get; set; }
        public DbSet<CayciKod> CayciKod { get; set; }
        public DbSet<Odeme> Odeme { get; set; }
        public DbSet<OdemeKullanici> OdemeKullanici { get; set; }
        public DbSet<AnaKategori> AnaKategori { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<KullaniciUrun> KullaniciUrun { get; set; }
        public DbSet<SepetSiparis> SepetSiparis { get; set; }
        public DbSet<SepetUrun> SepetUrun { get; set; }
        public DbSet<CayciRapor> CayciRapor { get; set; }
        public DbSet<MusteriRapor> MusteriRapor { get; set; }
        public DbSet<KullaniciFatura> KullaniciFatura { get; set; }
        public DbSet<KullaniciAidat> KullaniciAidat { get; set; }
    }
}