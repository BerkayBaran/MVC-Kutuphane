using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string p)
        {
            var prsnl = from u in db.TBL_Personel select u;
            if (!string.IsNullOrEmpty(p))
            {
                prsnl = prsnl.Where(x => x.Personel_Adi.Contains(p));
            }
            return View(prsnl.ToList());
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBL_Personel p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }
            if (db.TBL_Personel.Any(x => x.Personel_Kullanci_Adi == p.Personel_Kullanci_Adi))
            {
                ViewBag.Hata = "Kullanici Adi Sistemde Mevcut.";
                return View("PersonelEkle", p);
            }
            else
            {
                if (p.Cinsiyet == "E" || p.Cinsiyet == "K" || p.Cinsiyet == "B")
                {
                    p.Durum = true;
                    db.TBL_Personel.Add(p);
                    db.SaveChanges();
                }
                else
                {
                    p.Durum = true;
                    p.Cinsiyet = "B";
                    db.TBL_Personel.Add(p);
                    db.SaveChanges();
                }
                ViewBag.BasariliKayit = "Kayıt Olma Basarili!";
            }
            return View();
            
        }
        public ActionResult PersonelSil(int id)
        {
            var uye = db.TBL_Personel.Find(id);
            var odnc = db.TBL_OduncKitap.Where(a => a.Personel_ID == id).FirstOrDefault();
            if (odnc != null)
            {
                uye.Durum = false;
                return RedirectToAction("Index");
            }
            else
            {
                db.TBL_Personel.Remove(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
        public ActionResult PersonelGetir(int id)
        {
            var uye = db.TBL_Personel.Find(id);
            return View("PersonelGetir", uye);
        }
        public ActionResult PersonelGuncelle(TBL_Personel u)
        {
            var uye = db.TBL_Personel.Find(u.ID);
            uye.Personel_Adi = u.Personel_Adi;
            uye.Personel_Soyadi = u.Personel_Soyadi;
            uye.Personel_Kullanci_Adi = u.Personel_Kullanci_Adi;
            uye.Sifre = u.Sifre;
            uye.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}