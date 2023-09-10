using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class UyeduyuruController : Controller
    {
        // GET: Uyeduyuru
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var duyuru = db.Tbl_Duyurular.OrderByDescending(x => x.Tarih).Where(x => x.Durum == true).ToList();
            return View(duyuru);
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Duyurular.Find(id);
            return View("Detay", bul);

        }
    }
}