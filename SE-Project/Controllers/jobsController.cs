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
            var jobs = db.jobs.Include(j => j.user).Include(j => j.jobPic);
            return View(jobs.ToList());
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
            string totalAddress = "Address" + ", " + "City" + ", " + "State" + ", " + "Zipcode";

            if (ModelState.IsValid)
            {
                db.jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index", "organizer");
            }

            ViewBag.CreatedBy = new SelectList(db.users, "UserID", "FirstName", job.CreatedBy);
            ViewBag.JobID = new SelectList(db.jobPics, "JobID", "Before", job.JobID);
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
            ViewBag.JobID = new SelectList(db.jobPics, "JobID", "Before", job.JobID);
            return RedirectToAction("Index", "organizer");
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
            ViewBag.CreatedBy = new SelectList(db.users, "UserID", "FirstName", job.CreatedBy);
            ViewBag.JobID = new SelectList(db.jobPics, "JobID", "Before", job.JobID);
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
