using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCLIBRARY
{
    //BU SAYFADA PROJENİN ANASAYFA URLSINDE AÇILACAK OLAN SAYFANIN AYARI YAPILIR.
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Vitrine", action = "Index", id = UrlParameter.Optional } //PROJE BAŞLARKEN VİTRİNE CONTROLLER İÇİNDEKİ INDEX TEN BAŞLASIN.
            );
        }
    }
}
