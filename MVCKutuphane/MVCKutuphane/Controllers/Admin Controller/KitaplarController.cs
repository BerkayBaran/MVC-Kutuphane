using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers
{
    public class KitaplarController : Controller
    {
        // GET: Kitaplar
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index(string parametre)
        {
            var kitaplar = from k in db.TBL_Kitaplar select k;
            if (!string.IsNullOrEmpty(parametre))
            {
                kitaplar = kitaplar.Where(x => x.Kitap_Adi.Contains(parametre));
            }
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitaplarEkle()
        {
            List<SelectListItem> ktgdeger = (from i in db.TBL_Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategori_Adi,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr1 = ktgdeger;
            List<SelectListItem> yzrdeger = (from i in db.TBL_Yazar.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Yazar_Adi + ' ' + i.Yazar_Soyad,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr2 = yzrdeger;
            return View();
        }
        [HttpPost]
        public ActionResult KitaplarEkle(TBL_Kitaplar k)
        {
            var ktg = db.TBL_Kategori.Where(a => a.ID == k.TBL_Kategori.ID).FirstOrDefault();
            var yzr = db.TBL_Yazar.Where(y => y.ID == k.TBL_Yazar.ID).FirstOrDefault();
            k.TBL_Kategori = ktg;
            k.TBL_Yazar = yzr;
            db.TBL_Kitaplar.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitaplarSil(int id)
        {      
            var kitapsil = db.TBL_Kitaplar.Find(id);
            var ktp = db.TBL_OduncKitap.Where(a => a.Kitap_ID == id).FirstOrDefault();
            if (ktp != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                db.TBL_Kitaplar.Remove(kitapsil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult KitaplarGetir(int id)
        {
            var kitapgetir = db.TBL_Kitaplar.Find(id);
            List<SelectListItem> ktgdeger = (from i in db.TBL_Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategori_Adi,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr1 = ktgdeger;
            List<SelectListItem> yzrdeger = (from i in db.TBL_Yazar.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Yazar_Adi + ' ' + i.Yazar_Soyad,
                                                 Value = i.ID.ToString()
                                             }).ToList();
            ViewBag.dgr2 = yzrdeger;
            return View("KitaplarGetir", kitapgetir);
        }
        public ActionResult KitapGuncelle(TBL_Kitaplar a)
        {
          
                var kitap = db.TBL_Kitaplar.Find(a.ID);
                kitap.Kitap_Adi = a.Kitap_Adi;
                kitap.Sayfa_Sayisi = a.Sayfa_Sayisi;
                kitap.Yayin_Evi = a.Yayin_Evi;
                kitap.Basim_Yili = a.Basim_Yili;
                var ktg = db.TBL_Kategori.Where(k => k.ID == a.TBL_Kategori.ID).FirstOrDefault();
                var yzr = db.TBL_Yazar.Where(y => y.ID == a.TBL_Yazar.ID).FirstOrDefault();
                kitap.Kategori_ID = ktg.ID;
                kitap.Yazar_ID = yzr.ID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
