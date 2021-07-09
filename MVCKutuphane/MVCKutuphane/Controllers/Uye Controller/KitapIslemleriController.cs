using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers.Uye_Controller
{
    public class KitapIslemleriController : Controller
    {
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        // GET: KitapIslemleri
        public ActionResult Index(string parametre)
        {
            var kitaplar = from k in db.TBL_Kitaplar select k;
            if (!string.IsNullOrEmpty(parametre))
            {
                kitaplar = kitaplar.Where(x => x.Kitap_Adi.Contains(parametre));
            }
            var durum = db.TBL_Kitaplar.FirstOrDefault();
            return View(kitaplar.ToList());
        }

        public ActionResult Yazarlar(string parametre)
        {
            var yazarlar = from k in db.TBL_Yazar select k;
            if (!string.IsNullOrEmpty(parametre))
            {
                yazarlar = yazarlar.Where(x => x.Yazar_Adi.Contains(parametre));
            }
            var durum = db.TBL_Yazar.FirstOrDefault();
            return View(yazarlar.ToList());
        }
        /*
        public ActionResult KitapOduncAl(int id)
        {
            var kitapsil = db.TBL_Kitaplar.Find(id);
            kitapsil.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */
    }
}