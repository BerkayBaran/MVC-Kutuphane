using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string a)
        {
            var uye = from u in db.TBL_Uyeler select u;
            if (!string.IsNullOrEmpty(a))
            {
                uye = uye.Where(x => x.Uye_Adi.Contains(a) && x.Tur==1 );
            }
            return View(uye.ToList());
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBL_Uyeler u)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
            }
            if (db.TBL_Uyeler.Any(x => x.Kullanici_Adi == u.Kullanici_Adi || x.Uye_Mail == u.Uye_Mail))
            {
                ViewBag.Hata = "Kullanici Adi veya Email Sistemde Mevcut.";
                return View("UyeEkle", u);
            }
            else
            {
                if (u.Cinsiyet == "E" || u.Cinsiyet == "K" || u.Cinsiyet == "B")
                {
                    u.OduncDurum = true;
                    u.Bakiye = (decimal)0.0000;
                    db.TBL_Uyeler.Add(u);
                    db.SaveChanges();
                    ViewBag.BasariliKayit = "Kayıt Olma Basarili!";
                    return View();
                }
                else
                {
                    u.OduncDurum = true;
                    u.Bakiye = (decimal)0.0000;
                    u.Cinsiyet = "B";
                    db.TBL_Uyeler.Add(u);
                    db.SaveChanges();
                    ViewBag.BasariliKayit = "Kayıt Olma Basarili!";
                    return View();
                }
            }
                
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBL_Uyeler.Find(id);
            uye.Durum=false;
            uye.OduncDurum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBL_Uyeler.Find(id);
            return View("UyeGetir", uye);
        }
        public ActionResult UyeGuncelle(TBL_Uyeler u)
        {
            var uye = db.TBL_Uyeler.Find(u.ID);
            if (uye.OduncDurum == false)
            {
                uye.Uye_Adi = u.Uye_Adi;
                uye.Uye_Soyadi = u.Uye_Soyadi;
                uye.Uye_Mail = u.Uye_Mail;
                uye.Kullanici_Adi = u.Kullanici_Adi;
                uye.Sifre = u.Sifre;
                uye.Durum = true;
                uye.OduncDurum = false;
                uye.Tur = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                uye.Uye_Adi = u.Uye_Adi;
                uye.Uye_Soyadi = u.Uye_Soyadi;
                uye.Uye_Mail = u.Uye_Mail;
                uye.Kullanici_Adi = u.Kullanici_Adi;
                uye.Sifre = u.Sifre;
                uye.Durum = true;
                uye.OduncDurum = true;
                uye.Tur = 1;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }
    }
}