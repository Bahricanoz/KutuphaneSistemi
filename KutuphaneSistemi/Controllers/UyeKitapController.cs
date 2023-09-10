using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;
namespace KutuphaneSistemi.Controllers
{
    public class UyeKitapController : Controller
    {
        // GET: UyeKitap
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        [Authorize]
        public ActionResult Index()
        {
            var kullanici = Session["Kullaniciadi"].ToString();
            var geçmişkitaplar = db.Tbl_Hareket.Where(x => x.Tbl_Uye.Kullaniciadi == kullanici && x.Durum == true).ToList();
            return View(geçmişkitaplar);
        }
        public ActionResult KitapIndex()
        {
            var kullanici = Session["Kullaniciadi"].ToString();
            var kitaplarım = db.Tbl_Hareket.OrderByDescending(x=>x.Id).Where(x => x.Tbl_Uye.Kullaniciadi == kullanici && x.Durum == false).ToList();
            return View(kitaplarım);
        }
        public PartialViewResult Kitap()
        {
            List<SelectListItem> kitap = (from x in db.Tbl_Kitap.Where(x => x.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Kitapad
                                          }).ToList();
            ViewBag.kitap = kitap;
            return PartialView();
        }
        public PartialViewResult Per()
        {
            List<SelectListItem> per = (from x in db.Tbl_Personel.Where(x => x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Personelad + " " + x.Personelsoyad
                                        }).ToList();
            ViewBag.per = per;
            return PartialView();
        }
        [HttpGet]
        public ActionResult KitapAl()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult KitapAl(Tbl_Hareket p)
        {
            
            p.Durum = false;
            var kitap = db.Tbl_Kitap.Where(x => x.Id == p.Tbl_Kitap.Id).FirstOrDefault();
            var per = db.Tbl_Personel.Where(x => x.Id == p.Tbl_Personel.Id).FirstOrDefault();
            if (kitap != null)
            {
                // Kitap örneğini p.Tbl_Kitap'e atayın
                p.Tbl_Kitap = kitap;
                p.Tbl_Kitap.Durum = false;
                p.Tbl_Personel = per;
                db.Tbl_Hareket.Add(p);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Detay(Tbl_Kitap p)
        {
            var bul = db.Tbl_Kitap.FirstOrDefault(x => x.Id == p.Id);
            return View("Detay", bul);
            
        }
        [HttpGet]
        public ActionResult Iade(int id)
        {
            var bul = db.Tbl_Hareket.Find(id);
            return View("Iade", bul);
        }
        
        [HttpPost]
        public ActionResult Iade(Tbl_Hareket p)
        {
            var deger = db.Tbl_Hareket.Find(p.Id);
            deger.Durum = true;
            deger.Tbl_Kitap.Durum = true;
            deger.Teslimtarih = p.Teslimtarih;
            DateTime d1 = Convert.ToDateTime(deger.Alistarihi);
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan fark = d2 - d1;
            if(fark.TotalDays> 0)
            {
                Tbl_Ceza t = new Tbl_Ceza();
                t.uyeid = deger.Uyeid;
                t.Hareketid = deger.Id;
                t.Baslangic = deger.Alistarihi;
                t.Bitis = deger.Teslimtarih;
                t.Paracezası = Convert.ToDecimal(15 * fark.TotalDays);
                db.Tbl_Ceza.Add(t);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}