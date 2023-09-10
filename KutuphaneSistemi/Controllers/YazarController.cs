using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var yazar = db.Tbl_Yazar.ToList();
            return View(yazar);
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Yazar.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Yazar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            p.Durum = true;
            db.Tbl_Yazar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Yazar.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Yazar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }
            var deger = db.Tbl_Yazar.Find(p.Id);
            deger.Durum = true;
            deger.Yazarad = p.Yazarad;
            deger.Soyad = p.Soyad;
            deger.Detay = p.Detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Yazar.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Yazar.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Kitaplar(int id)
        {
            var bul = db.Tbl_Kitap.Where(x => x.Yazarid == id).ToList();
            var yazarad = db.Tbl_Kitap.FirstOrDefault(x => x.Yazarid == id);
            ViewBag.ad = yazarad.Tbl_Yazar.Yazarad + " " + yazarad.Tbl_Yazar.Soyad;
            return View("Kitaplar", bul);
        }
    }
}