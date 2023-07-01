using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLIBRARY.Models.Entity;
using System.Web.Security;

namespace MVCLIBRARY.Controllers
{
    [AllowAnonymous] //BU KOMUT İLE PROJE BAZINDA AUTHORIZE OZELLIGINDEN BU SAYFA MUAF TUTULMUŞTUR. 
    public class LoginController : Controller
    {
        LIBRARYDBEntities db = new LIBRARYDBEntities();
        public ActionResult Login()
        {
            return View();
        }         
        /*  Session nedir nerede kullanılır?
                Sessionlar oturum bilgilerini saklamak için kullanılan yapılardır. 
                Hepimiz illa ki bir siteye üye olup giriş yapmışızdır. 
                İşte giriş yaptığımız sırada kullanıcı adımız ve şifremiz kontrol edilir. 
                Eğer doğruysa bilgilerimiz session'a atanır.
            */
        [HttpPost]
        public ActionResult Login(TBL_USER p)
        {
            var degerler = db.TBL_USER.FirstOrDefault(x=> x.MAIL == p.MAIL && x.PASSWORD==p.PASSWORD);                //girilen mail ve şifre ile uyuşan veritabanındaki ilk kayıt çekilir değerler adlı değişkene atanır.
            if(degerler!= null)                                                                                               //kayıt null değilse kayıttaki ad soyad vb. bilgiler session parametrelerine aktarılır. 
            {
                FormsAuthentication.SetAuthCookie(degerler.MAIL, false);
                Session["mail"] = degerler.MAIL.ToString();                                                               //sessionla çekeceğim tek değer mail olacak.maili tutacak
                //TempData["id"] = degerler.ID.ToString();
                TempData["name"] = degerler.NAME.ToString();
                TempData["surname"] = degerler.SURNAME.ToString();
                TempData["username"] = degerler.USERNAME.ToString();
                TempData["password"] = degerler.PASSWORD.ToString();
                TempData["school"] = degerler.SCHOOL.ToString();
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                return View();
            }           
        }




    }
}