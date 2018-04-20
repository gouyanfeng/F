using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            return View();
        }
        public void RabbitSend(string message)
        {
            // new RabbitHelper().PubEasy(message, "easy.log");
            new RabbitHelper().SendEasy(RabbitQueueEnum.ExceptionLog, message);

        }

    }
}


