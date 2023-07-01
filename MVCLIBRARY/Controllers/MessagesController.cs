using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;


namespace MVCLIBRARY.Controllers
{

    public class MessagesController : Controller
    {
        // GET: Messages

        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBL_MESSAGES.Where(x => x.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        public ActionResult GidenMesajlar()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBL_MESSAGES.Where(x => x.GÖNDEREN == uyemail.ToString()).ToList();
            return View(mesajlar);
        }



        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBL_MESSAGES t)
        {  
            
            var uyemail = (string)Session["Mail"].ToString();
            t.GÖNDEREN = uyemail.ToString();
            t.TARİH = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TBL_MESSAGES.Add(t);
            db.SaveChanges();
            return RedirectToAction("GidenMesajlar", "Messages");
        }



        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();

            int gelenMesajSayısı = db.TBL_MESSAGES.Where(x => x.ALICI == uyemail).Count();
            ViewBag.gelenMesajSayısı = gelenMesajSayısı;

            int gidenMesajSayısı = db.TBL_MESSAGES.Where(x => x.GÖNDEREN == uyemail).Count();
            ViewBag.gidenMesajSayısı = gidenMesajSayısı;




            return PartialView();
        }
    }
}