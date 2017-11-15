using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SE_Project.Controllers
{
    public class volunteerController : Controller
    {
        // GET: volunteer
        //[Authorize]
        public ActionResult Index()
        {
            if (Session["isLoggedIn"] == "yes")
            {
                int accessLevel = (int)Session["userAccess"];

                if (accessLevel == 1 || accessLevel == 3)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "organizer");
                }
                
            }
            else
            {
                return RedirectToAction("Login", "users");
            }
            
        }
    }
}