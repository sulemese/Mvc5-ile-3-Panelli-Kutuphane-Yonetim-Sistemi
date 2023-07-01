using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
namespace MVCLIBRARY.Controllers
{
    public class ProcessController : Controller
    {
        /*------------------------------------------------------------------------*/
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        /*------------------------------------------------------------------------*/
            //HAREKET TABLOSUNDA ÖDÜNÇ İADESİ ALINDIĞINDA KİTAPLARIN DURUMU TRUE YA DÖNER. 
            //DURUMU TRUE OLAN KİTAPLAR ARTIK İADESİ YAPILDIĞI İÇİN İŞLEMLER TABLOSUNDA ARŞİVLENİR.
        public ActionResult Index()
        {
            var degerler = db.TBL_DEPOSIT.Where(x => x.TRANSACTIONSTATUS == true).ToList();
            return View(degerler);
        }
        /*------------------------------------------------------------------------*/

    }
}