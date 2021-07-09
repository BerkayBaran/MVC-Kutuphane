using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
using System.Web.Security;
using System.Security;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace MVCKutuphane.Controllers
{
    public class GirisController : Controller
    {
        // GET: Giris
        DBKutuphaneEntities db = new DBKutuphaneEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TBL_Uyeler u , TBL_Personel p)
        {
          
            var bilgiler = db.TBL_Uyeler.FirstOrDefault(x => x.Kullanici_Adi == u.Kullanici_Adi && x.Sifre == u.Sifre);
            var girisbilgisiUye = db.TBL_Uyeler.FirstOrDefault(x => x.Kullanici_Adi == u.Kullanici_Adi && x.Sifre == u.Sifre && x.Tur == 1);
            var personelBilgi = db.TBL_Uyeler.FirstOrDefault(x => x.Kullanici_Adi == u.Kullanici_Adi && x.Sifre == u.Sifre);         
            var girisbilgisiPersonel = db.TBL_Uyeler.FirstOrDefault(x => x.Kullanici_Adi == u.Kullanici_Adi && x.Sifre == u.Sifre && x.Tur == 2);
            if (bilgiler != null && girisbilgisiUye!=null && girisbilgisiPersonel==null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici_Adi,false);
                Session["KullaniciAdi"] = bilgiler.Kullanici_Adi.ToString();
                Session["userId"] = bilgiler.ID;
                return RedirectToAction("Index", "UyePaneli");
            }
            else if (personelBilgi != null && girisbilgisiPersonel != null && girisbilgisiUye == null)
            {
                /*
                FormsAuthentication.SetAuthCookie(personelBilgi.Personel_Kullanci_Adi, false);
                Session["KullaniciAdi"] = personelBilgi.Personel_Kullanci_Adi.ToString();
                */
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici_Adi, false);
                Session["KullaniciAdi"] = bilgiler.Kullanici_Adi.ToString();
                Session["userId"] = bilgiler.ID;
                return RedirectToAction("Index", "AnaSayfa");
            }
            else
            {
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult SifreyiResetle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifreyiResetle(TBL_Uyeler u)
        {
            var model = db.TBL_Uyeler.Where(x => x.Uye_Mail == u.Uye_Mail && x.ipucu==u.ipucu).FirstOrDefault();
            if (model != null)
            {
                Guid random = Guid.NewGuid();
                model.Sifre = random.ToString().Substring(0, 8);
                db.SaveChanges();
                
                MailMessage mailMessage = new MailMessage();

                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Sayın " + model.Kullanici_Adi + ",<br/><br/>");
                sbEmailBody.Append("<br/>"); sbEmailBody.Append("Şifreniz: " + model.Sifre);
                sbEmailBody.Append("<br/><br/>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Şifreniz";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "Cibillies535@gmail.com",
                    Password = "51bSy7BEC54nBc191"
                };
                string to = model.Uye_Mail;
                string from = "Cibillies535@gmail.com";

                smtpClient.EnableSsl = true;

                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(to);
                smtpClient.Send(mailMessage);
                smtpClient.UseDefaultCredentials = false;
                return RedirectToAction("Index", "Giris");
            }
            return View();
        }
        public static System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }

    }
}  

