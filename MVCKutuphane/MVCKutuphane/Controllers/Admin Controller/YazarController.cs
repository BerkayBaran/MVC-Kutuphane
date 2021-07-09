using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string y)
        {
            var yazarlar = from k in db.TBL_Yazar select k;
            if (!string.IsNullOrEmpty(y))
            {
                yazarlar = yazarlar.Where(x => x.Yazar_Adi.Contains(y));
            }
            return View(yazarlar.ToList());
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TBL_Yazar d)
        {
            db.TBL_Yazar.Add(d);
            db.SaveChanges();
            return View();
        }
        public ActionResult YazarSil(int id)
        {
            var yazarsil = db.TBL_Yazar.Find(id);
            var ktp = db.TBL_Kitaplar.Where(a => a.Yazar_ID == id).FirstOrDefault();
            if (ktp != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.TBL_Yazar.Remove(yazarsil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult YazarGetir(int id)
        {
            var yazargetir = db.TBL_Yazar.Find(id);
            return View("YazarGetir", yazargetir);
        }
        public ActionResult YazarGuncelle(TBL_Yazar y)
        {
            var yzr = db.TBL_Yazar.Find(y.ID);
            yzr.Yazar_Adi = y.Yazar_Adi;
            yzr.Yazar_Soyad = y.Yazar_Soyad;
            yzr.Kitap_Sayisi = y.Kitap_Sayisi;
            yzr.Detay = y.Detay;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}