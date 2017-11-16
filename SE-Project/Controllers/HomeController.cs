using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((string)Session["isLoggedIn"] == "yes")
            {
                return View();
            }
            else
            {
                Session["isLoggedIn"] = "no";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}