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
    public class organizerController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: organizer
        public ActionResult Index()
        {
            int id = (int) Session["userID"];
            var list = db.jobs.Where(x => x.CreatedBy == id);

            return View(list);
        }
    }
}