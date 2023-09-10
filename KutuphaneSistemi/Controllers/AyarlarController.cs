using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var admin = db.Tbl_Admin.ToList();
            return View(admin);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Admin.FirstOrDefault(x => x.Id == id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Admin.FirstOrDefault(x => x.Id == id);
            db.Tbl_Admin.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Admin.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Admin p)
        {
            var deger = db.Tbl_Admin.FirstOrDefault(x => x.Id == p.Id);
            deger.Sifre = p.Sifre;
            deger.Durum = true;
            deger.Yetki = p.Yetki;
            deger.Kullaniciadi = p.Kullaniciadi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Admin p)
        {
            p.Durum = true;
            db.Tbl_Admin.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}