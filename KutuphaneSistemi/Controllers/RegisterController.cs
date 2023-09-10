using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Uyekayıt()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Uyekayıt(Tbl_Uye p)
        {
            p.Durum = false;
            db.Tbl_Uye.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}