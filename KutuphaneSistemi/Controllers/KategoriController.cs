using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var kategori = db.Tbl_Kategori.ToList();
            return View(kategori);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Kategori p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            p.Durum = true;
            db.Tbl_Kategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Kategori.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Kategori p)
        {
            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }
            var deger = db.Tbl_Kategori.Find(p.Id);
            deger.Durum = true;
            deger.KategoriAd = p.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}