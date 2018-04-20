using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
    public class WechatController : Controller
    {
        //
        // GET: /Wechat/

        public ActionResult Index()
        {
            //MSMQ.Enqueue("");
            WeixinJsHelper v = new WeixinJsHelper("wx6beb9f98ef116fd2", "94a5d1f1a1c365bcf60f0b3e1ca97e82", "ccc");
            return View();
        }

    }
}
