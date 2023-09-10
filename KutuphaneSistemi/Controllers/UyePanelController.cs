using KutuphaneSistemi.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneSistemi.Controllers
{
    public class UyePanelController : Controller
    {
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        // GET: UyePanel
        [HttpGet]
        [Authorize]
        
        public ActionResult Index()
        {
            var kullanıcı = (string)Session["Kullaniciadi"];
            var deger = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == kullanıcı);
            return View(deger);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Uye p)
        {
            var kullanıcı = (string)Session["Kullaniciadi"];
            var uye = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == kullanıcı);
            uye.Sifre = p.Sifre;
            uye.Telefon = p.Telefon;
            uye.Mail = p.Mail;
            uye.Okul = p.Okul;
            uye.Yas = p.Yas;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Hesap()
        {
            var kullaniciadi = Session["kullaniciadi"].ToString();
            var deger = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == kullaniciadi);
            return View(deger);
        }
        public ActionResult Hakkımızda()
        {
            var hakkımızda = db.Tbl_Hakkımızda.ToList();
            return View(hakkımızda);
        }
        public ActionResult Yardım()
        {
            return View();
        }
    }
}