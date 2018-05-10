using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            CityService cityService = new CityService();
            cityService.Create();

            var a = 1;

            var c = A();




            a = 1;

            var ccc = await c;
            return View();
        }

        public ActionResult About(AAA aaa)
        {



            return View();
        }

        public ActionResult Contact(AAA aaa)
        {


            return View();
        }


        public async Task<int> A()
        {
            return await Task.Run(() =>
            {

                System.Threading.Thread.Sleep(1000 * 5);
                return 1;

            });
        }

    }
}