using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var duyurular = db.Tbl_Duyurular.OrderByDescending(x => x.Tarih).ToList();
            return View(duyurular);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            db.Tbl_Duyurular.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Duyurular p)
        {
            p.Durum = true;
            db.Tbl_Duyurular.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Duyurular p)
        {
            var deger = db.Tbl_Duyurular.Find(p.Id);
            deger.Durum = true;
            deger.Konu = p.Konu;
            deger.Icerik = p.Icerik;
            deger.Tarih = Convert.ToDateTime(DateTime.Now);
            db.SaveChanges ();
            return RedirectToAction("Index");
        }
    }
}