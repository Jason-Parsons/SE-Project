using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using SE_Project.Models;


namespace SE_Project.Controllers
{
    public class volunteerController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();


        public ActionResult Index()
        {
            if ((string)Session["isLoggedIn"] == "yes")
            {
                int accessLevel = (int)Session["userAccess"];

                 var list = db.jobs;

                var user = (int)Session["userID"];
                if (accessLevel == 1 || accessLevel == 3)
                {
                    return View(list);
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