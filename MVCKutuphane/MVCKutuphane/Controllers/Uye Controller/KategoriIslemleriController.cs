using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers.Uye_Controller
{
    public class KategoriIslemleriController : Controller
    {
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        // GET: KategoriIslemleri
        public ActionResult Index(string parametre)
        {

            var kategori = from k in db.TBL_Kategori select k;
            if (!string.IsNullOrEmpty(parametre))
            {
                kategori = kategori.Where(x => x.Kategori_Adi.Contains(parametre));
            }
            var durum = db.TBL_Kategori.FirstOrDefault();
            return View(kategori.ToList());
        }
    }
}