using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            var degerler = db.TBL_OduncKitap.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> kitap = (from i in db.TBL_Kitaplar.Where(u => u.Durum == true).ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.Kitap_Adi,
                                              Value = i.ID.ToString()
                                          }).ToList();
            ViewBag.dgr1 = kitap;



            List<SelectListItem> uye = (from i in db.TBL_Uyeler.ToList().Where(u => u.Tur == 1 && u.Durum == true && u.OduncDurum==true && u.Bakiye>=(decimal)5.0000)
                                        select new SelectListItem
                                        {
                                            Text = i.Uye_Adi + ' ' + i.Uye_Soyadi,
                                            Value = i.ID.ToString()
                                        }).ToList();
            ViewBag.dgr2 = uye;

            List<SelectListItem> personel = (from i in db.TBL_Personel.ToList().Where(u =>u.Durum == true)
                                        select new SelectListItem
                                        {
                                            Text = i.Personel_Adi + ' ' + i.Personel_Soyadi,
                                            Value = i.ID.ToString()
                                        }).ToList();
            ViewBag.dgr3 = personel;

            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBL_OduncKitap o, TBL_Kasa k)
        {
            var ktp = db.TBL_Kitaplar.Where(a => a.ID == o.TBL_Kitaplar.ID).FirstOrDefault();
            var uye = db.TBL_Uyeler.Where(y => y.ID == o.TBL_Uyeler.ID).FirstOrDefault();
            var bilgiler = db.TBL_Personel.FirstOrDefault(x => x.ID == o.TBL_Personel.ID);
            
            
            o.TBL_Kitaplar = ktp;
            o.TBL_Uyeler = uye;
            o.TBL_Personel = bilgiler;

            if (ktp.Durum == true && uye.Bakiye >=5.0000m && uye.OduncDurum==true && uye.Durum==true && uye.OduncDurum==true &&  o.Teslim_Tarihi==null)
            {
                /*
                FormsAuthentication.SetAuthCookie(bilgiler.Personel_Kullanci_Adi, false);
                Session["KullaniciAdi"] = bilgiler.Personel_Kullanci_Adi.ToString();
                */
                ktp.Durum = false;
                uye.OduncDurum = false;
                k.Miktar += 5.0000m;
                db.TBL_Kasa.Add(k);
                uye.Bakiye -= 5.0000m;
                db.TBL_OduncKitap.Add(o);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(int id ,TBL_OduncKitap a)
        {
            var odunc = db.TBL_OduncKitap.Find(id);
            odunc.TBL_Kitaplar.Durum = true;
            odunc.TBL_Uyeler.OduncDurum = true;
            //db.TBL_OduncKitap.Remove(odunc);
            odunc.Teslim_Tarihi = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}