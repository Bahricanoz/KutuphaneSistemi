using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class OduncKitapAlController : Controller
    {
        // GET: OduncKitapAl
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var odunckitap = db.Tbl_Hareket.OrderByDescending(x=>x.Id).Where(x=>x.Durum== false).ToList();
            return View(odunckitap);
        }
        [HttpGet]
        public ActionResult Teslimet(int id)
        {
            var bul = db.Tbl_Hareket.Find(id);
            return View("Teslimet", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Hareket p)
        {
            
            var deger = db.Tbl_Hareket.Find(p.Id);
            deger.Durum = true;
            deger.Tbl_Kitap.Durum = true;
            deger.Teslimtarih = p.Teslimtarih;
            DateTime dt1 = DateTime.Parse(deger.İadetarih.ToString());
            DateTime dt2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan day = dt2 - dt1;
            if(day.TotalDays > 0)
            {
                Tbl_Ceza t = new Tbl_Ceza();
                t.uyeid = deger.Uyeid;
                t.Hareketid = p.Id;
                t.Baslangic = p.Alistarihi;
                t.Bitis = p.Teslimtarih;
                t.Paracezası = Convert.ToDecimal(15 * (day.TotalDays));
                db.Tbl_Ceza.Add(t);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}