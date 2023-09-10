using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var resimler = db.Tbl_Kitap.OrderByDescending(x=>x.Id).Take(6).ToList();
            return View(resimler);
        }
        public PartialViewResult Hakkımda()
        {
            var hakkımda = db.Tbl_Hakkımızda.ToList();
            return PartialView(hakkımda);
        }
        [HttpGet]
        public PartialViewResult Mesaj()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Mesaj(Tbl_Mesaj p)
        {
            p.Tarih = DateTime.Now;
            db.Tbl_Mesaj.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}