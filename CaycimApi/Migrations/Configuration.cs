using CaycimApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaycimApi.Migrations
{
    public class Configuration : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        int i=1;
        int count()
        {
            return i++;
        }
        protected override void Seed(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            context.Roles.Add(new IdentityRole()
            {
                Name = "Admin"
            });
            context.Roles.Add(new IdentityRole()
            {
                Name = "Çaycı"
            });
            context.Roles.Add(new IdentityRole()
            {
                Name = "Müşteri"
            });

            IList<AnaKategori> AnaKategori = new List<AnaKategori>();

            AnaKategori.Add(new AnaKategori()
            {
                Adi="Sıcaklar"
            });
            AnaKategori.Add(new AnaKategori()
            {
                Adi = "Soğuklar"
            });
            AnaKategori.Add(new AnaKategori()
            {
                Adi = "Yiyecekler"
            });
            context.AnaKategori.AddRange(AnaKategori);

            IList<Urun> Urun = new List<Urun>();

            Urun.Add(new Urun()
            {
                UrunAdi = "Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/cay/orta.png",
                Oncelik = 1
                
            }
);

            Urun.Add(new Urun()
            {
                UrunAdi = "Açık Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/cay/acik.png",
                Oncelik = 2
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Demli Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/cay/kapali.png",
                Oncelik = 3
            }
            );









            Urun.Add(new Urun()
            {
                UrunAdi = "Ayva Ihlamur Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/ayvaihlamur.png",
                Oncelik = 23
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Ekinezya Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/ekinezya.png",
                Oncelik =24
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Ihlamur Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/ihlamur.png",
                Oncelik = 12
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limon Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/limon.png",
                Oncelik = 25
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Melisa Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/melisa.png",
                Oncelik = 26
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Nane Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/nane.png",
                Oncelik = 27
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Nane Limon Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/nanelimon.png",
                Oncelik = 13
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Papatya Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/papatya.png",
                Oncelik = 28
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Rezine Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/rezine.png",
                Oncelik = 29
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Tarçın Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/tarcin.png",
                Oncelik = 30
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Yasemin Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/yasemin.png",
                Oncelik = 31
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Zencefil Çayı",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/bitkisel/zencefil.png",
                Oncelik = 32
            }
            );











            Urun.Add(new Urun()
            {
                UrunAdi = "Ayran",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/diger/ayran.png",
                Oncelik = 35
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Bardak Su",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/diger/bsu.png",
                Oncelik = 33
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limonata",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/diger/limonata.png",
                Oncelik = 36
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şişe Su",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/diger/su.png",
                Oncelik = 34
            }
            );







            Urun.Add(new Urun()
            {
                UrunAdi = "Çilek Kavun Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/cilekkavun.png",
                Oncelik = 49
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Fanta",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/fanta.png",
                Oncelik = 44
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/gazoz.png",
                Oncelik = 45
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kırmızı Meyve Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/kirmizimeyve.png",
                Oncelik = 50
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kola",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/kola.png",
                Oncelik = 43
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Light Kola",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/light.png",
                Oncelik = 47
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Light Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/lightgazoz.png",
                Oncelik = 51
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limon Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/limonlugazoz.png",
                Oncelik = 52
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Nar Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/narligazoz.png",
                Oncelik = 53
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Portakal Gazoz",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/portakalligazoz.png",
                Oncelik = 48
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Zero Kola",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/gazli/zero.png",
                Oncelik = 46
            }
            );















            Urun.Add(new Urun()
            {
                UrunAdi = "Dereotlu Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/dereotlup.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/kasarlip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kıymalı Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/kiymalip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Patatesli Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/patateslip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Peynir Dereotlu Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/peynirlidereotlup.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Peynir Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/peynirlip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Sosisli Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/sosislip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Sucuklu Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/sucuklup.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Zeytinli Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/zeytinlip.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Poğaça",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/sadea.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Açma",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/kasarlia.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Peynirli Açma",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/peynirlia.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Zeytinli Açma",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/zeytinlia.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Açma",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/sadea.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Simit",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/hamur/simit.png",
                Oncelik = count()
            }
            );



















            Urun.Add(new Urun()
            {
                UrunAdi = "Ananas İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/ananas.png",
                Oncelik = 54
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Bergamot İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/bergamot.png",
                Oncelik = 42
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Çilek İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/cilek.png",
                Oncelik = 55
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Elma İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/elma.png",
                Oncelik = 56
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Karpuz İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/karpuz.png",
                Oncelik = 57
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Karpuz Kavun İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/karpuzkavun.png",
                Oncelik = 58
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limon İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/limon.png",
                Oncelik = 40
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Mango İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/mango.png",
                Oncelik = 41
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şeftali İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/seftali.png",
                Oncelik = 39
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şeftali Kayısı İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/seftalikayisi.png",
                Oncelik = 59
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Yeşilçay İcetea",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/icetea/yesilcay.png",
                Oncelik = 60
            }
            );














            Urun.Add(new Urun()
            {
                UrunAdi = "Sade Türk Kahvesi",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahve/normal.png",
                Oncelik = 4
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Orta Türk Kahvesi",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahve/orta.png",
                Oncelik = 5
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şekerli Türk Kahvesi",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahve/sekerli.png",
                Oncelik = 6
            }
            );
            Urun.Add(new Urun()
            {
                UrunAdi = "2si 1 Arada",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahveler/2_1.png",
                Oncelik = 7
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "3ü 1 Arada",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahveler/3_1.png",
                Oncelik = 8
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Klasik Kahve",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahveler/clasic.png",
                Oncelik = 9
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Gold Kahve",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahveler/gold.png",
                Oncelik = 10
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Latte",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/kahveler/latte.png",
                Oncelik = 11
            }
            );















            Urun.Add(new Urun()
            {
                UrunAdi = "Ananaslı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/ananas.png",
                Oncelik = 61
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Atom Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/atom.png",
                Oncelik = 62
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Egzotik Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/egzotik.png",
                Oncelik = 63
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Elmalı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/elma.png",
                Oncelik = 64
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Karışık Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/karisik.png",
                Oncelik = 65
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Karpuzlu Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/karpuz.png",
                Oncelik = 66
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kavunlu Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/kavun.png",
                Oncelik = 67
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kayısılı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/kayisi.png",
                Oncelik = 68
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kirazlı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/kiraz.png",
                Oncelik = 69
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limonlu Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/limon.png",
                Oncelik = 70
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Mandalinalı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/mandalina.png",
                Oncelik = 71
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Narlı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/nar.png",
                Oncelik = 72
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Portakallı Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/portakal.png",
                Oncelik = 73
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şeftalili Meyve Suyu",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/meyvasulari/seftali.png",
                Oncelik = 74
            }
            );

















            Urun.Add(new Urun()
            {
                UrunAdi = "Çilekli Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/cilek.png",
                Oncelik = 75
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Elmalı Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/elma.png",
                Oncelik = 76
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kavunlu Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/kavun.png",
                Oncelik = 77
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kirazlı Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/kiraz.png",
                Oncelik = 78
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limonlu Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/limon.png",
                Oncelik = 38
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Mandalinalı Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/mandalina.png",
                Oncelik = 79
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Narlı Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/nar.png",
                Oncelik = 80
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/sade.png",
                Oncelik = 37
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Şeftalili Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/seftali.png",
                Oncelik = 81
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Vişneli Soda",
                AnaKategori = AnaKategori[1],
                IconUrl = "../assets/images/urunlogo/soda/visne.png",
                Oncelik = 82
            }
            );





















            Urun.Add(new Urun()
            {
                UrunAdi = "Karışık Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/karisik.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/kasar.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Kavurmalı Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/kasarkavurma.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Sucuklu Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/kasarsucuk.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kaşarlı Sucuklu Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/kavurma.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Sucuklu Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/sucuk.png",
                Oncelik = count()
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Sucuklu Kavurmalı Tost",
                AnaKategori = AnaKategori[2],
                IconUrl = "../assets/images/urunlogo/tost/sucukkavurma.png",
                Oncelik = count()
            }
            );


















            Urun.Add(new Urun()
            {
                UrunAdi = "Sıcak Çikolata",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/cikolata.png",
                Oncelik = 11
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Elmalı Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/elma.png",
                Oncelik = 14
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Karadutlu Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/karadut.png",
                Oncelik = 15
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Kuşburnulu Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/kusburnu.png",
                Oncelik = 16
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Limonlu Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/limon.png",
                Oncelik = 17
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Muzlu Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/muz.png",
                Oncelik = 18
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Narlı Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/nar.png",
                Oncelik = 19
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Portakallı Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/portakal.png",
                Oncelik = 20
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Vişneli Çay",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/visne.png",
                Oncelik = 21
            }
            );

            Urun.Add(new Urun()
            {
                UrunAdi = "Salep",
                AnaKategori = AnaKategori[0],
                IconUrl = "../assets/images/urunlogo/tozlar/salep.png",
                Oncelik = 22
            }
            );

            context.Urun.AddRange(Urun);

            //if (!context.Users.Any(u => u.UserName == "ayfer"))
            //var user0 = new ApplicationUser() { UserName = "safak", Email = "safak@gmail.com", Name = "Şafak", SurName = "Demir", PhoneNumber = "0000000000", CompanyName = "Kırık Ekran", Gender = "Erkek",Enable=true };
            var user = new ApplicationUser() { UserName = "celal258", Email = "celal258@gmail.com", Name = "Celalettin", SurName = "Demir", PhoneNumber = "1111111111", CompanyName = "Atmaca Aş.", Gender = "Erkek", Enable = true };
            var user1 = new ApplicationUser() { UserName = "hamza", Email = "hamza@gmail.com", Name = "Hamza", SurName = "Bilici", PhoneNumber = "2222222222", CompanyName = "Lol Aş.", Gender = "Kadın", Enable = true };
            var user2 = new ApplicationUser() { UserName = "mert", Email = "mert@gmail.com", Name = "Mert", SurName = "Albayrak", PhoneNumber = "3333333333", CompanyName = "Sofit Aş.", Gender = "Erkek", Enable = false };
            var user3 = new ApplicationUser() { UserName = "muhtar", Email = "muhtar@gmail.com", Name = "Muhtar", SurName = "Sade", PhoneNumber = "4444444444", CompanyName = "Kantar Aş.", Gender = "Kadın", Enable = true };
            //manager.Create(user0, "123456");
            //manager.AddToRole(user0.Id, "Admin");

            manager.Create(user, "123456");
            manager.AddToRole(user.Id, "Çaycı");

            manager.Create(user1, "123456");
            manager.AddToRole(user1.Id, "Müşteri");

            manager.Create(user2, "123456");
            manager.AddToRole(user2.Id, "Çaycı");

            manager.Create(user3, "123456");
            manager.AddToRole(user3.Id, "Müşteri");


            IList<KullaniciUrun> KullaniciUrun = new List<KullaniciUrun>();

            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci= user,
                Urun= Urun[0],
                Fiyat=1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[1],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[2],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[3],
                Fiyat = 1,
                isPublish = true
            });

            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[5],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[6],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user,
                Urun = Urun[7],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[0],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[1],
                Fiyat = 2,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[2],
                Fiyat = 1,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[3],
                Fiyat = 2,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[4],
                Fiyat = 2,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[5],
                Fiyat = 2,
                isPublish = true
            });
            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[9],
                Fiyat = 2,
                isPublish = true
            });

            KullaniciUrun.Add(new KullaniciUrun()
            {
                Cayci = user2,
                Urun = Urun[10],
                Fiyat = 5,
                isPublish = true
            });
            context.KullaniciUrun.AddRange(KullaniciUrun);

            IList<MusteriRapor> MusteriRapor = new List<MusteriRapor>();

            MusteriRapor.Add(new MusteriRapor()
            {
                Musteri= user1,
                OdenenTutar=0,
                ToplamTutar=0,
            });
            MusteriRapor.Add(new MusteriRapor()
            {
                Musteri = user3,
                OdenenTutar = 0,
                ToplamTutar = 0,
            });
            context.MusteriRapor.AddRange(MusteriRapor);

            IList<CayciRapor> CayciRapor = new List<CayciRapor>();

            CayciRapor.Add(new CayciRapor()
            {
                Cayci = user,
                TahsilatTutar = 0,
                ToplamTutar = 0,
            });
            CayciRapor.Add(new CayciRapor()
            {
                Cayci = user2,
                TahsilatTutar = 0,
                ToplamTutar = 0,
            });
            context.CayciRapor.AddRange(CayciRapor);

            IList<CayciMusteri> CayciMusteri = new List<CayciMusteri>();

            CayciMusteri.Add(new CayciMusteri()
            {
                Cayci = user,
                Musteri=user1
            });
            CayciMusteri.Add(new CayciMusteri()
            {
                Cayci = user2,
                Musteri = user3
            });
            context.CayciMusteri.AddRange(CayciMusteri);

            IList<SepetSiparis> SepetSiparis = new List<SepetSiparis>();

            IList<SepetUrun> SepetUrun = new List<SepetUrun>();

            SepetUrun.Add(new SepetUrun()
            {
                Adet = 1,
                KullaniciUrun = KullaniciUrun[0],
                Fiyat = KullaniciUrun[0].Fiyat*1
            });
            SepetUrun.Add(new SepetUrun()
            {
               Adet=2,
                KullaniciUrun= KullaniciUrun[1],
                Fiyat= KullaniciUrun[1].Fiyat * 2
            });
            SepetUrun.Add(new SepetUrun()
            {
                Adet = 3,
                KullaniciUrun = KullaniciUrun[2],
                Fiyat = KullaniciUrun[2].Fiyat * 3
            });
            SepetSiparis.Add(new SepetSiparis()
            {
                SepetUruns=SepetUrun,
                IsConfirm = false,
                Tarih = DateTime.Now,
                Not = "Çaylar Taze Olsun",
                Cayci = user,
                Musteri = user1,
                ToplamFiyat = SepetUrun.Sum(p => p.Fiyat),
                IsPaid = false,
            });


            SepetUrun = new List<SepetUrun>();

            SepetUrun.Add(new SepetUrun()
            {
                Adet = 1,
                KullaniciUrun = KullaniciUrun[5],
                Fiyat = KullaniciUrun[5].Fiyat * 1
            });
            SepetUrun.Add(new SepetUrun()
            {
                Adet = 2,
                KullaniciUrun = KullaniciUrun[6],
                Fiyat = KullaniciUrun[6].Fiyat * 2
            });
            SepetUrun.Add(new SepetUrun()
            {
                Adet = 3,
                KullaniciUrun = KullaniciUrun[7],
                Fiyat = KullaniciUrun[7].Fiyat * 3
            });
            SepetSiparis.Add(new SepetSiparis()
            {
                SepetUruns=SepetUrun,
                IsConfirm = false,
                Tarih = DateTime.Now,
                Not = "Çaylar bayat Olsun",
                Cayci = user2,
                Musteri = user3,
                ToplamFiyat = SepetUrun.Sum(p=>p.Fiyat),
                IsPaid = false,
            });

            context.SepetSiparis.AddRange(SepetSiparis);
            base.Seed(context);
        }
    }
}