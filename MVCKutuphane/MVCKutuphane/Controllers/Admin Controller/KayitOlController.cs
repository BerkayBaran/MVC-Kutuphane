using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers.Admin_Controller
{
    public class KayitOlController : Controller
    {
        // GET: Kayit
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        [HttpGet]
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TBL_Uyeler u)
        {
            if (db.TBL_Uyeler.Any(x => x.Kullanici_Adi == u.Kullanici_Adi || x.Uye_Mail==u.Uye_Mail))
            {
                ViewBag.Hata = "Kullanici Adi veya Email Sistemde Mevcut.";
                return View("Kayit", u);
            }
            else
            {
                if(u.Cinsiyet=="E" || u.Cinsiyet=="K" || u.Cinsiyet == "B")
                {
                    u.Durum = true;
                    u.Tur = 1;
                    u.OduncDurum = true;
                    u.Bakiye = (decimal)0.0000;
                    db.TBL_Uyeler.Add(u);
                    db.SaveChanges();
                }
                else
                {
                    u.Durum = true;
                    u.Tur = 1;
                    u.Cinsiyet = "B";
                    u.OduncDurum = true;
                    u.Bakiye = (decimal)0.0000;
                    db.TBL_Uyeler.Add(u);
                    db.SaveChanges();
                }
                
            }
            ModelState.Clear();
            ViewBag.BasariliKayit = "Kayıt Olma Basarili!";
            return View("Kayit", new TBL_Uyeler());
        }
    }
}