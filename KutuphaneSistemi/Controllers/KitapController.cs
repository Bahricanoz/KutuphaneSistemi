using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Mvc;
using KutuphaneSistemi.Models.Entitiy;

namespace KutuphaneSistemi.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        Db_KutuphaneEntities db = new Db_KutuphaneEntities();
        public ActionResult Index()
        {
            var kitap = db.Tbl_Kitap.ToList();
            return View(kitap);
        }
        public ActionResult Aktif(int id)
        {
            var bul = db.Tbl_Kitap.Find(id);
            bul.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Pasif(int id)
        {
            var bul = db.Tbl_Kitap.Find(id);
            bul.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Detay(int id)
        {
            var bul = db.Tbl_Kitap.Find(id);
            return View("Detay", bul);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> kat = (from x in db.Tbl_Kategori.Where(x=>x.Durum == true).ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.KategoriAd
                                        }).ToList();

            ViewBag.kat = kat;
            List<SelectListItem> yazar = (from x in db.Tbl_Yazar.Where(x => x.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Value = x.Id.ToString(),
                                              Text = x.Yazarad + " " + x.Soyad
                                          }).ToList();
            ViewBag.yaz = yazar;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Tbl_Kitap p)
        {
            var ktg = db.Tbl_Kategori.Where(x => x.Id == p.Tbl_Kategori.Id).FirstOrDefault();
            var yazar = db.Tbl_Yazar.Where(x => x.Id == p.Tbl_Yazar.Id).FirstOrDefault();
            p.Tbl_Kategori = ktg;
            p.Tbl_Yazar = yazar;
            p.Durum = true;
            db.Tbl_Kitap.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var bul = db.Tbl_Kitap.Find(id);
            List<SelectListItem> kat = (from x in db.Tbl_Kategori.ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.KategoriAd
                                        }).ToList();
            ViewBag.kat = kat;
            List<SelectListItem> yaz = (from x in db.Tbl_Yazar.ToList()
                                        select new SelectListItem
                                        {
                                            Value = x.Id.ToString(),
                                            Text = x.Yazarad + " " + x.Soyad
                                        }).ToList();
            ViewBag.yaz = yaz;
            return View("Guncelle", bul);
        }
        [HttpPost]
        public ActionResult Guncelle(Tbl_Kitap p)
        {
            var kat = db.Tbl_Kategori.Where(x => x.Id == p.Tbl_Kategori.Id).FirstOrDefault();
            var yaz = db.Tbl_Yazar.Where(x => x.Id == p.Tbl_Yazar.Id).FirstOrDefault();
            var deger = db.Tbl_Kitap.Find(p.Id);
            deger.Sayfasayisi = p.Sayfasayisi;
            deger.Durum = true;
            deger.Kitapad = p.Kitapad;
            deger.Basyımyılı = p.Basyımyılı;
            deger.Yayinevi = p.Yayinevi;
            deger.Katid = kat.Id;
            deger.Yazarid = yaz.Id;
            deger.Foto = p.Foto;
            deger.Detay = p.Detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}