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
    public class jobsController : Controller
    {
        private VolsDBEntities db = new VolsDBEntities();

        // GET: jobs
        public ActionResult Index()
        {
            if ((string)Session["isLoggedIn"] == "yes")
            {
                int accessLevel = (int)Session["userAccess"];
                int id = (int)Session["userID"];
                var list = db.jobs.Where(x => x.CreatedBy == id);

                if (accessLevel == 2 || accessLevel == 3)
                {
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Index", "volunteer");
                }
            }

            else
            {
                return RedirectToAction("Login", "users");
            }
        }

        // GET: jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
           ViewBag.CreatedBy = new SelectList(db.users, "UserID", "FirstName");
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,Address,City,State,Zipcode,LocationLat,LocationLong,Date,Time,Description,NumVolsNeeded,CreatedBy")] job job)
        {
            if (ModelState.IsValid)
            {
                // var geocoder = new google.maps.Geocoder();
                // private string src = "https://maps.googleapis.com/maps/api/js?key=AIzaSyBsV_j3UnupXN7XKDIIldbskUI8IbhnNuw&callback=initMap";
                 
                db.jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index", "organizer");
            }
            int id = (int)Session["userID"];
            ViewBag.CreatedBy = new SelectList(db.users.Where(x => x.UserID == id), "UserID", "FirstName", job.CreatedBy);
            
            return View(job);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatedBy = new SelectList(db.users, "UserID", "FirstName", job.CreatedBy);
            
            return View(job);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,Address,City,State,Zipcode,LocationLat,LocationLong,Date,Time,Description,NumVolsNeeded,CreatedBy")] job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatedBy = Session["userID"];
            
            return View(job);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            job job = db.jobs.Find(id);
            db.jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
