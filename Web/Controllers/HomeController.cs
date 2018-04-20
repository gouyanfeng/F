using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string a = "aaaaaaa";
            string b = "aaaaaaa";
            string c = "aaaaaaa";
            return View();
        }

        public ActionResult About(AAA aaa)
        {

            aaa.A = "1111111";

            return View();
        }

        public ActionResult Contact(AAA aaa)
        {


            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}