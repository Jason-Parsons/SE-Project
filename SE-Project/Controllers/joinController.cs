using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SE_Project.Models;

namespace SE_Project.Controllers
{
    public class joinController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: volunteers
        public ActionResult Index()
        {
            return View();
        }
    }

}