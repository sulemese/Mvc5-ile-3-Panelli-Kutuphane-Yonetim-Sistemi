using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCLIBRARY.Models.Entity;

namespace MVCLIBRARY.Controllers
{
    //BU AUTHORIZE EN ÜSTTE TANIMLANDIĞI İÇİN BU CONTROLLER İÇİNDE TÜM METODLARI KAPSAR.
    [Authorize]  //KULLANICI URL KISMINDAN PANEL/INDEX  veya PANEOL/DUYURULAR VB. YAZIP DİREKT PANELE GİTMEK İSTERSE LOGİN EKRANINA YÖNLENDİREN AUTHORİZE 

    public class PanelController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();


        [HttpGet]
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TBL_USER.FirstOrDefault(z=>z.MAIL == uyemail);
            var degerler = db.TBL_NOTIFICATION.ToList();
            //BU KOD CONTROLLERDA OTURUM AÇAN KİŞİNİN MAİLİ İLE UYUŞAN KAYITTAN AD BİLGİSİNİ ÇEKER VE VİEW KATMANINA TAŞIR. 
            var d1 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.NAME).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.SURNAME).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.PHOTOGRAPH).FirstOrDefault();
            ViewBag.d3 = d3;
            var d4 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.USERNAME).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.SCHOOL).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.TELEPHONENUM).FirstOrDefault();
            ViewBag.d6 = d6;
            var d7 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;



            var uyeID = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(Z=>Z.ID).FirstOrDefault();

            var kitapSayisi = db.TBL_DEPOSIT.Where(z => z.USERID == uyeID).Count();
            ViewBag.d8 = kitapSayisi;

            var duyuruSayisi = db.TBL_NOTIFICATION.Count();
            ViewBag.d9 = duyuruSayisi;

            var mesajSayisi = db.TBL_MESSAGES.Where(z => z.ALICI == uyemail).Count();
            ViewBag.d10 = duyuruSayisi;

            return View(degerler);
        }







        //LOGİN CONTROLLERINDA GELEN DEĞERİ PANEL CONTROLLERINDA GÜNCELLEYECEĞİZ.
        //SESSİON DAN MAİL İ ÇEKTİK.
        //USER A ATADIK.
        //VERİTABANINDAN MAİLİ USER OLAN KULLANICININ KAYDINI USR A ATADIK.
        // PARAMETREDEN GELEN PAROLAYI USR IN PAROLASINA ATADIK. ARTIK USR YENİ PAROLAYI TUTUYOR.
        // DEĞİŞİKLİLERİ VERİ TABANINA KAYDETTİK. 
        [HttpPost]
        public ActionResult Index2(TBL_USER p)
        {
            var user = (string)Session["Mail"];  
            var usr = db.TBL_USER.FirstOrDefault(x=>x.MAIL == user);
            usr.PASSWORD = p.PASSWORD;
            db.SaveChanges();
            return RedirectToAction("Index", "Panel");           
        }


   
        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBL_USER.Where(x => x.MAIL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault();
            var degerler = db.TBL_DEPOSIT.Where(x => x.USERID == id).ToList();
            return View(degerler);
        }


       
        public ActionResult Duyurular()
        {
            var duyurular = db.TBL_NOTIFICATION.ToList();
            return View(duyurular);
        }



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }


        //PARTİAL VİEW ÖNEMLİ BİR KONUDUR.
        //BİR SAYDADA BİRDEN FAZLA VİEW GÖRÜNTÜLENMESİNİ SAĞLAR.
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            var uyeMail = (string)Session["Mail"];
            var id = db.TBL_USER.Where(x => x.MAIL == uyeMail).Select(x => x.ID).FirstOrDefault();
            var uyebul = db.TBL_USER.Find(id);
            return PartialView("Partial2",uyebul);
        }

        public ActionResult SidebarPicture()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TBL_USER.FirstOrDefault(z=>z.MAIL == uyemail);
            var degerler = db.TBL_NOTIFICATION.ToList();
            //BU KOD CONTROLLERDA OTURUM AÇAN KİŞİNİN MAİLİ İLE UYUŞAN KAYITTAN AD BİLGİSİNİ ÇEKER VE VİEW KATMANINA TAŞIR. 
            var d1 = db.TBL_USER.Where(z => z.MAIL == uyemail).Select(z => z.PHOTOGRAPH).FirstOrDefault();
            ViewBag.d1 = d1;
            return View();
        }
    }
}