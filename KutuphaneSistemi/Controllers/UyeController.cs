using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var uye = db.Tbl_Uye.OrderByDescending(x => x.Id).ToList();
            return View(uye);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Uye p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            p.Durum = true;
            db.Tbl_Uye.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Uye.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Uye p)
        {
            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }
            var deger = db.Tbl_Uye.Find(p.Id);
            deger.Ad = p.Ad;
            deger.Soyad = p.Soyad;
            deger.Yas = p.Yas;
            deger.Okul = p.Okul;
            deger.Mail = p.Mail;
            deger.Telefon = p.Telefon;
            deger.Durum = true;
            deger.Kullaniciadi = p.Kullaniciadi;
            deger.Sifre = p.Sifre;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitaplar(int id)
        {
            var bul = db.Tbl_Hareket.Where(x => x.Uyeid == id).ToList();
            var uyead = db.Tbl_Hareket.FirstOrDefault(x => x.Tbl_Uye.Id == id);
            ViewBag.uyead = uyead.Tbl_Uye.Ad + " " + uyead.Tbl_Uye.Soyad;
            return View(bul);
        }
    }
}