using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index()
        {

            new CityService().GetList();
            return View();
        }

    }
}
