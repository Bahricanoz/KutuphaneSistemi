using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;

namespace KutuphaneSistemi.Controllers
{
    public class islemlerController : Controller
    {
        // GET: islemler
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var islem = db.Tbl_Hareket.OrderByDescending(x=>x.Teslimtarih).Where(x => x.Durum == true).ToList();
            return View(islem);
        }
    }
}