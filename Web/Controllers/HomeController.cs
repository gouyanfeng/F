using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            CityService cityService = new CityService();
            cityService.Create();

            var ticket = new FormsAuthenticationTicket("FormsAuthenticationTicket", true, 1);



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


    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Password { get; set; }
        public BasicAuthenticationIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;
        }
       
    }
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        { }
    }
}