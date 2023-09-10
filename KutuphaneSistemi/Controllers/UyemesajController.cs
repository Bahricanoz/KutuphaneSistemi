using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class UyemesajController : Controller
    {
        // GET: Uyemesaj
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var mail = Session["Mail"].ToString();
            var mesaj = db.Tbl_Mesaj.Where(x=>x.Alıcımail == mail).ToList();
            return View(mesaj);
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
            return RedirectToAction("Index");
        }
        public ActionResult Mesaj()
        {
            var mail = Session["Mail"].ToString();
            var mesajlar = db.Tbl_Mesaj.Where(x => x.Mail == mail).ToList();
            return View(mesajlar);
        }
    }
}