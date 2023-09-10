using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class OduncKitapController : Controller
    {
        // GET: OduncKitap
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Uye()
        {
            List<SelectListItem> uye = (from x in db.Tbl_Uye.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Ad + " " + x.Soyad
                                        }).ToList();
            ViewBag.uye = uye;
            return PartialView();
        }
        public PartialViewResult Kitap()
        {
            List<SelectListItem> kitap = (from y in db.Tbl_Kitap.Where(x => x.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Value = y.Id.ToString(),
                                              Text = y.Kitapad
                                          }).ToList();
            ViewBag.kitap = kitap;
            return PartialView();
        }
        public PartialViewResult Per()
        {
            List<SelectListItem> per = (from z in db.Tbl_Personel.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = z.Id.ToString(),
                                            Text = z.Personelad + " " + z.Personelsoyad
                                        }).ToList();
            ViewBag.per = per;
            return PartialView();
        }
        [HttpGet]
        public ActionResult Kitapver()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Kitapver(Tbl_Hareket p)
        {
            var ktg = db.Tbl_Kitap.Where(x => x.Id == p.Tbl_Kitap.Id).FirstOrDefault();
            var uye = db.Tbl_Uye.Where(x => x.Id == p.Tbl_Uye.Id).FirstOrDefault();
            var per = db.Tbl_Personel.Where(x => x.Id == p.Tbl_Personel.Id).FirstOrDefault();
            p.Tbl_Personel = per;
            p.Tbl_Uye = uye;
            p.Tbl_Kitap = ktg;
            p.Tbl_Kitap.Durum = false;
            p.Durum = false;
            db.Tbl_Hareket.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}