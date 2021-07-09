using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string p)
        {
            var kategoriler = from k in db.TBL_Kategori select k;
            if (!string.IsNullOrEmpty(p))
            {
                kategoriler = kategoriler.Where(x => x.Kategori_Adi.Contains(p));
            }
            return View(kategoriler.ToList());
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(TBL_Kategori a)
        {
            db.TBL_Kategori.Add(a);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return View();
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBL_Kategori.Find(id);
            var ktp = db.TBL_Kitaplar.Where(a => a.Kategori_ID == id).FirstOrDefault();
            if (ktp != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.TBL_Kategori.Remove(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBL_Kategori.Find(id);
            return View("KategoriGetir", ktg);
        }
        public ActionResult KategoriGuncelle(TBL_Kategori b)
        {
            var ktg = db.TBL_Kategori.Find(b.ID);
            ktg.Kategori_Adi = b.Kategori_Adi;
            ktg.Aciklama = b.Aciklama;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}