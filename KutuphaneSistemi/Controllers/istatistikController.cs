using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        
        public ActionResult Index()
        {
            var uye = db.Tbl_Uye.Count();
            var kitap = db.Tbl_Kitap.Count();
            var emanetkitap = db.Tbl_Kitap.Where(x => x.Durum == false).Count();
            var para = db.Tbl_Ceza.Sum(x => x.Paracezası);
            ViewBag.para = para;
            ViewBag.uye = uye;
            ViewBag.kitap = kitap;
            ViewBag.emanetkitap = emanetkitap;
            return View();
        }
        public ActionResult Havakartı()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        public ActionResult Sorgular()
        {
            var kitap = db.Tbl_Kitap.Count();
            ViewBag.kitap = kitap;

            var uye = db.Tbl_Uye.Count();
            ViewBag.uye = uye;

            var emanet = db.Tbl_Kitap.Where(x => x.Durum == false).Count();
            ViewBag.emanet = emanet;

            var mesaj = db.Tbl_Mesaj.Count();
            ViewBag.mesaj = mesaj;

            var kategori = db.Tbl_Kategori.Count();
            ViewBag.kat = kategori;

            var para = db.Tbl_Ceza.Sum(x=>x.Paracezası);
            ViewBag.para = para;

            var okunakitap = db.Tbl_Hareket
                        .GroupBy(x => x.Tbl_Kitap.Kitapad)
                        .OrderByDescending(z => z.Count())
                        .Select(y => y.Key )
                        .FirstOrDefault();
            ViewBag.encokokunan = okunakitap.ToString();

            var yazar = db.Tbl_Kitap
                        .GroupBy(x => new { x.Tbl_Yazar.Yazarad, x.Tbl_Yazar.Soyad })
                        .OrderByDescending(z => z.Count())
                        .Select(y => y.Key)
                        .FirstOrDefault();
            ViewBag.yazar = $"{yazar.Yazarad} {yazar.Soyad}";

            var yayınevi = db.Tbl_Kitap
                          .GroupBy(x => x.Yayinevi)
                          .OrderByDescending(z => z.Count())
                          .Select(y => y.Key)
                          .FirstOrDefault();
            ViewBag.yayınevi = yayınevi.ToString();

            var personel = db.Tbl_Hareket
                        .GroupBy(x => new { x.Tbl_Personel.Personelad, x.Tbl_Personel.Personelsoyad }) // Hem adı hem de soyadı bazında gruplama
                        .OrderByDescending(z => z.Count())
                        .Select(y => y.Key)
                        .FirstOrDefault();
            ViewBag.per = $"{personel.Personelad} {personel.Personelsoyad}";
            // bugun emanet edilen kitap sayısı
            var sayı = db.Tbl_Hareket.Where(x => x.Alistarihi == DateTime.Today).Count();
            ViewBag.sayı = sayı;
            // en aktif üye
            var aktifuye = db.Tbl_Hareket
                          .GroupBy(x => new { x.Tbl_Uye.Ad, x.Tbl_Uye.Soyad })
                          .OrderByDescending(z => z.Count())
                          .Select(y => y.Key)
                          .FirstOrDefault();
            ViewBag.aktifuye = $"{aktifuye.Ad} {aktifuye.Soyad}";

            return View();
        }
    }
}