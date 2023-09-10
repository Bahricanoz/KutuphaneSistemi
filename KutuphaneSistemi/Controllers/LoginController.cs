using KutuphaneSistemi.Models.Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models;
using System.Web.Security;

namespace KutuphaneSistemi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminGiris(Tbl_Admin p)
        {
            var bilgiler = db.Tbl_Admin.FirstOrDefault(x => x.Kullaniciadi == p.Kullaniciadi && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullaniciadi, false);
                return RedirectToAction("Index", "istatistik");
            }
            else
            {
                return RedirectToAction("AdminGiris");
            }
        }
        public ActionResult AdminCikisyap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("AdminGiris", "Login");
        }
        [HttpGet]
        public ActionResult UyeGiris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeGiris(Tbl_Uye p)
        {
            var bilgi = db.Tbl_Uye.FirstOrDefault(x => x.Kullaniciadi == p.Kullaniciadi && x.Sifre == p.Sifre && x.Durum == true);
            if (bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.Kullaniciadi, false);
                Session["Kullaniciadi"] = bilgi.Kullaniciadi.ToString();
                Session["Ad"] = bilgi.Ad.ToString();
                Session["Soyad"] = bilgi.Soyad.ToString();
                Session["Mail"] = bilgi.Mail.ToString();
                Session["Yas"] = bilgi.Yas.ToString();
                Session["Telefon"] = bilgi.Telefon.ToString();
                Session["Id"] = bilgi.Id.ToString();
                Session["Okul"] = bilgi.Okul.ToString();
                Session["Sifre"] = bilgi.Sifre.ToString();
                return RedirectToAction("Index","UyePanel");
            }
            else
            {
                return RedirectToAction("UyeGiris", "Login");
            }
        }
        public ActionResult Uyecikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Uyekayıt","Register");
        }
    }
}