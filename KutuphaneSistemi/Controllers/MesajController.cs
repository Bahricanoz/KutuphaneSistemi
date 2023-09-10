using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class MesajController : Controller
    {
        // GET: Mesaj
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var mesajlar = db.Tbl_Mesaj.OrderByDescending(x => x.Id).ToList();
            return View(mesajlar);
        }
        public ActionResult Sil(int id)
        {
            var bul = db.Tbl_Mesaj.Find(id);
            db.Tbl_Mesaj.Remove(bul);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Mesaj.Find(id);
            return View("Detay", bul);
        }
        
        [HttpGet]
        public ActionResult Gonder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Gonder(Tbl_Mesaj p)
        {
            db.Tbl_Mesaj.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Mesaj");
        }
    }
}