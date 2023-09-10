using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class HakkımızdaController : Controller
    {
        // GET: Hakkımızda
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var hakkımızda = db.Tbl_Hakkımızda.ToList();
            return View(hakkımızda);
        }
        [HttpPost]
        public ActionResult Index(Tbl_Hakkımızda p)
        {
            var deger = db.Tbl_Hakkımızda.Find(1);
            deger.icerik1 = p.icerik1;
            deger.icerik2 = p.icerik2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}