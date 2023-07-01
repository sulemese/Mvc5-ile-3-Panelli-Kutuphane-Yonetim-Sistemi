using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models;

namespace MVCLIBRARY.Controllers
{
   
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KitapSonuçlarınıGörselleştir()
        {
            return Json(liste());
        }

        public List<Class1> liste()
        {
            List<Class1> cs = new List<Class1>();
            cs.Add(new Class1() { yayınevi = "Güneş", sayı = 7 });
            cs.Add(new Class1() { yayınevi = "Mars", sayı = 4 });
            cs.Add(new Class1() { yayınevi = "Jupiter", sayı = 3 });
            return cs;
        }
    }
}