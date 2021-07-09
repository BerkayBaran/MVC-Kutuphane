using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
namespace MVCKutuphane.Controllers.Uye_Controller
{
    public class BakiyeController : Controller
    {
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        // GET: Bakiye
        public ActionResult Index()
        {          
            var userId = (int)Session["userId"];
            var user = db.TBL_Uyeler.Find(userId);
            var degerler = db.TBL_Uyeler.Where(u => u.ID == userId);
            return View(degerler.Where(t => t.ID == userId).ToList());
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(TBL_Uyeler o)
        {
            var uye = db.TBL_Uyeler.Find(o.ID);         
            uye.Bakiye += o.Bakiye;
            if (o.Bakiye > 0)
            {
                db.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
      
    }
}