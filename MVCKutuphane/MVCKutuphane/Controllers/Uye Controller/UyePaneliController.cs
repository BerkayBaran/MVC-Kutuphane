using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers.Uye_Controller
{
    public class UyePaneliController : Controller
    {
        // GET: UyePaneli
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var uyekullaniciadi = (string)Session["KullaniciAdi"];
            var degerler = db.TBL_Uyeler.First(x => x.Kullanici_Adi == uyekullaniciadi);
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBL_Uyeler u)
        {
            var uye = (string)Session["KullaniciAdi"];
            var bilgiler = db.TBL_Uyeler.FirstOrDefault(x=>x.Kullanici_Adi == uye);
            bilgiler.Sifre = u.Sifre;
            bilgiler.Uye_Adi = u.Uye_Adi;
            bilgiler.Uye_Soyadi = u.Uye_Soyadi;
            bilgiler.Uye_Mail = u.Uye_Mail;
            bilgiler.ipucu = u.ipucu;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Kitaplarim()
        {
            var uye = (string)Session["KullaniciAdi"];
            var id = db.TBL_Uyeler.Where(x => x.Kullanici_Adi == uye.ToString()).Select(z=>z.ID).FirstOrDefault();
            var degerler = db.TBL_OduncKitap.Where(x => x.Uye_ID == id).ToList();
            return View(degerler);
        }
    }
}