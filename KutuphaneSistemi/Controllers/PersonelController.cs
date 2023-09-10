using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;

namespace KutuphaneSistemi.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var personel = db.Tbl_Personel.ToList();
            return View(personel);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
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
        public ActionResult Ekle(Tbl_Personel p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            p.Durum = true;
            db.Tbl_Personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Personel.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Personel p)
        {
            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }
            var deger = db.Tbl_Personel.Find(p.Id);
            deger.Durum = true;
            deger.Personelsoyad = p.Personelsoyad;
            deger.Personelad = p.Personelad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}