using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SE_Project.Models;
using PagedList;

namespace SE_Project.Controllers
{
    public class HomeController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

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

        public ActionResult Contact(int? id)
        {
            ViewBag.Message = "Your application description page.";
            job job = db.jobs.Find(id);
            if (id == null)
            {
                id = 1;
                return View(job);
            }
            return View(job);
        }

        public ActionResult ContactNext(int? id)
        {
            job job = db.jobs.Find(id);
            if (id == null)
            {
                id = 1;
                return View(job);
            }
            else
            {
                id++;
                return View(job);
            }
        }
        public ActionResult ContactBefore(int? id)
        {
            job job = db.jobs.Find(id);
            if (id == null)
            {
                id = 1;
                return View(job);
            }
            else
            {
                id--;
                return View(job);
            }
        }
    }
}