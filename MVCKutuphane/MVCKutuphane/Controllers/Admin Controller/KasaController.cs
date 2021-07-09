using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;

namespace MVCKutuphane.Controllers.Admin_Controller
{
    public class KasaController : Controller
    {
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        // GET: Kasa
        public ActionResult Index()
        {
            var degerler = db.TBL_Kasa.ToList();
            var toplam = db.TBL_Kasa.Sum(x => x.Miktar);
            ViewBag.Toplam = toplam;
            return  View ();
        }
    }
}